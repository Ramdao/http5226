using Microsoft.AspNetCore.Mvc;
using System;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using Passion_Project.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passion_Project.Services
{
    public class UserTimelineService : IUserTimeline
    {
        private readonly ApplicationDbContext _context;

        public UserTimelineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserTimelineDto>> GetTimelinesForUser(int userId)
        {
            List<UserTimelineDto> result = new();

            // Check if the user exists
            bool userExists = await _context.users.AnyAsync(u => u.user_Id == userId);
            if (!userExists)
            {
                return result; // Return empty list if user not found
            }

            try
            {
                result = await _context.UsersTimeline
                    .Where(ut => ut.user_id == userId)
                    .Join(_context.timelines,
                          ut => ut.timeline_Id,
                          t => t.timeline_Id,
                          (ut, t) => new UserTimelineDto
                          {
                              usertime_Id = ut.usertime_Id,
                              timeline_Id = t.timeline_Id,
                              user_id = ut.user_id,
                              timeline_name = t.timeline_name
                          })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
            }

            return result;
        }

        public async Task<IEnumerable<UserTimelineDto>> GetUsersForTimeline(int timelineId)
        {
            List<UserTimelineDto> result = new();

            // Check if the timeline exists
            bool timelineExists = await _context.timelines.AnyAsync(t => t.timeline_Id == timelineId);
            if (!timelineExists)
            {
                return result; // Return empty list if timeline not found
            }

            try
            {
                result = await _context.UsersTimeline
                    .Where(ut => ut.timeline_Id == timelineId)
                    .Join(_context.users,
                          ut => ut.user_id,
                          u => u.user_Id,
                          (ut, u) => new UserTimelineDto
                          {
                              usertime_Id = ut.usertime_Id,
                              timeline_Id = ut.timeline_Id,
                              user_id = u.user_Id,
                              first_name = u.first_name, 
                              last_name = u.last_name   
                          })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
            }

            return result;
        }

        public async Task<ServiceResponse> LinkUserToTimeline(int userId, int timelineId)
        {
            ServiceResponse serviceResponse = new();

            // Check if both user and timeline exist
            bool userExists = await _context.users.AnyAsync(u => u.user_Id == userId);
            bool timelineExists = await _context.timelines.AnyAsync(t => t.timeline_Id == timelineId);

            if (!userExists || !timelineExists)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                if (!userExists)
                {
                    serviceResponse.Messages.Add("User was not found.");
                }
                if (!timelineExists)
                {
                    serviceResponse.Messages.Add("Timeline was not found.");
                }
                return serviceResponse;
            }

            // Check if the relationship already exists to avoid duplicates
            bool linkExists = await _context.UsersTimeline.AnyAsync(ut => ut.user_id == userId && ut.timeline_Id == timelineId);
            if (linkExists)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("This user is already linked to the specified timeline.");
                return serviceResponse;
            }

            try
            {
                // Create a new entry in the UserTimeline join table
                UserTimeline userTimeline = new()
                {
                    user_id = userId,
                    timeline_Id = timelineId
                };

                await _context.UsersTimeline.AddAsync(userTimeline);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an issue linking the user to the timeline.");
                serviceResponse.Messages.Add(ex.Message);
                return serviceResponse;
            }

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            return serviceResponse;
        }

        public async Task<ServiceResponse> UnlinkUserFromTimeline(int userId, int timelineId)
        {
            ServiceResponse serviceResponse = new();

            // Find the existing link
            UserTimeline? userTimeline = await _context.UsersTimeline
                .FirstOrDefaultAsync(ut => ut.user_id == userId && ut.timeline_Id == timelineId);

            if (userTimeline == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("The user is not linked to this timeline.");
                return serviceResponse;
            }

            try
            {
                _context.UsersTimeline.Remove(userTimeline);
                await _context.SaveChangesAsync();

                serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
                serviceResponse.Messages.Add("User successfully unlinked from the timeline.");
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an issue unlinking the user from the timeline.");
                serviceResponse.Messages.Add(ex.Message);
            }

            return serviceResponse;
        }


    }

    
}
