using Microsoft.AspNetCore.Mvc;
using System;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using Passion_Project.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Add this import to use LINQ methods like `Include` and `Where`

namespace Passion_Project.Services
{
    public class EntriesService : IEntriesService
    {
        private readonly ApplicationDbContext _context;

        public EntriesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EntriesDto>> List()
        {
            // Include the Timeline data when querying entries
            var entries = await _context.entries
                .Include(e => e.Timeline) // Make sure to include the associated Timeline
                .ToListAsync();

            var entriesDtos = entries.Select(e => new EntriesDto()
            {
                entries_Id = e.entries_Id,
                entries_name = e.entries_name,
                description = e.description,
                location = e.location,
                images = e.images,
                timeline_Id = e.timeline_Id,
                Timeline = new TimelineDto()
                {
                    timeline_Id = e.Timeline.timeline_Id,
                    timeline_name = e.Timeline.timeline_name, // Add timeline_name here
                    date = e.Timeline.date,
                    description = e.Timeline.description
                }
            }).ToList();

            return entriesDtos;
        }

        public async Task<EntriesDto?> FindEntry(int id)
        {
            // Find entry by {id}, including the associated Timeline data
            var entry = await _context.entries
                .Include(e => e.Timeline)
                .FirstOrDefaultAsync(e => e.entries_Id == id);

            if (entry == null)
            {
                return null;
            }

            return new EntriesDto()
            {
                entries_Id = entry.entries_Id,
                entries_name = entry.entries_name,
                description = entry.description,
                location = entry.location,
                images = entry.images,
                timeline_Id = entry.timeline_Id,
                Timeline = new TimelineDto()
                {
                    timeline_Id = entry.Timeline.timeline_Id,
                    timeline_name = entry.Timeline.timeline_name,
                    date = entry.Timeline.date,
                    description = entry.Timeline.description
                }
            };
        }

        public async Task<ServiceResponse> UpdateEntry(int id, EntriesDto entryDto)
        {
            ServiceResponse serviceResponse = new();

            if (string.IsNullOrWhiteSpace(entryDto.entries_name) || entryDto.timeline_Id <= 0)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Entry name and a valid timeline ID are required.");
                return serviceResponse;
            }

            var entry = await _context.entries.FindAsync(id);
            if (entry == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("Entry not found.");
                return serviceResponse;
            }

            entry.entries_name = entryDto.entries_name;
            entry.description = entryDto.description;
            entry.location = entryDto.location;
            entry.images = entryDto.images;
            entry.timeline_Id = entryDto.timeline_Id;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Updating Entry failed.");
                return serviceResponse;
            }

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;
        }

        public async Task<ServiceResponse> AddEntry(EntriesDto entryDto)
        {
            ServiceResponse serviceResponse = new();

            if (string.IsNullOrWhiteSpace(entryDto.entries_name) || entryDto.timeline_Id <= 0)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Entry name and a valid timeline ID are required.");
                return serviceResponse;
            }

            Entries entry = new Entries()
            {
                entries_name = entryDto.entries_name,
                description = entryDto.description,
                location = entryDto.location,
                images = entryDto.images,
                timeline_Id = entryDto.timeline_Id,
            };

            try
            {
                _context.entries.Add(entry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Entry.");
                serviceResponse.Messages.Add(ex.Message);
            }

            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = entry.entries_Id;
            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteEntry(int id)
        {
            ServiceResponse response = new();

            var entry = await _context.entries.FindAsync(id);
            if (entry == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Entry cannot be deleted because it does not exist.");
                return response;
            }

            try
            {
                _context.entries.Remove(entry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the Entry.");
                return response;
            }

            response.Status = ServiceResponse.ServiceStatus.Deleted;
            return response;
        }

        // NEW METHOD: GetEntriesForTimeline
        public async Task<IEnumerable<EntriesDto>> GetEntriesForTimeline(int timelineId)
        {
            var entries = await _context.entries
                .Include(e => e.Timeline) // Ensure we include Timeline
                .Where(e => e.timeline_Id == timelineId)
                .ToListAsync();

            List<EntriesDto> entriesDtos = new List<EntriesDto>();

            foreach (var entry in entries)
            {
                entriesDtos.Add(new EntriesDto()
                {
                    entries_Id = entry.entries_Id,
                    entries_name = entry.entries_name,
                    description = entry.description,
                    location = entry.location,
                    images = entry.images,
                    timeline_Id = entry.timeline_Id,
                    Timeline = new TimelineDto()
                    {
                        timeline_Id = entry.Timeline.timeline_Id,
                        timeline_name = entry.Timeline.timeline_name,
                        date = entry.Timeline.date,
                        description = entry.Timeline.description
                    }
                });
            }

            return entriesDtos;
        }
    }
}
