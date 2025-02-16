using Microsoft.AspNetCore.Mvc;
using System;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using Passion_Project.Data;
using Microsoft.EntityFrameworkCore;

namespace Passion_Project.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> List()
        {
            // All users
            List<Users> users = await _context.users.ToListAsync();
            List<UserDto> UserDtos = new List<UserDto>();

            foreach (Users user in users)
            {
                // Create new instance UserDto and add to list
                UserDtos.Add(new UserDto()
                {
                    user_Id = user.user_Id,
                    first_name = user.first_name,
                    last_name = user.last_name,
                    email = user.email,
                    friend_list = user.friend_list,
                    password= user.password
                });
            }

            // return UserDto
            return UserDtos;
        }

        public async Task<UserDto?> FindUser(int id)
        {

            // Finds User based on {id}
            var user = await _context.users.FirstOrDefaultAsync(c => c.user_Id == id);

            if (user == null)
            {
                return null;
            }

            // Create an instance of UserDto
            UserDto userDto = new UserDto()
            {

                user_Id = user.user_Id,
                first_name = user.first_name,
                last_name = user.last_name,
                email = user.email,
                friend_list= user.friend_list,
                password=user.password


            };


            return userDto;
        }



        public async Task<ServiceResponse> UpdateUser(int id, UserDto userdto)
        {
            ServiceResponse serviceResponse = new();
            // Check for empty required fields
            if (string.IsNullOrWhiteSpace(userdto.first_name) ||
                string.IsNullOrWhiteSpace(userdto.last_name) ||
                string.IsNullOrWhiteSpace(userdto.email))
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Please provide valid first name, last name, and email.");
                return serviceResponse;
            }

            // Find existing user in the database
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                serviceResponse.Messages.Add("User not found.");
                return serviceResponse;
            }

            // Update properties
            user.first_name = userdto.first_name;
            user.last_name = userdto.last_name;
            user.email = userdto.email;
            user.friend_list = userdto.friend_list;
            user.password = userdto.password;

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Updating User failed.");
                return serviceResponse;
            }

            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;
        }

        public async Task<ServiceResponse> AddUser(UserDto userdto)
        {

            ServiceResponse serviceResponse = new();

            // Check for empty required fields
            if (string.IsNullOrWhiteSpace(userdto.first_name) ||
                string.IsNullOrWhiteSpace(userdto.last_name) ||
                string.IsNullOrWhiteSpace(userdto.email) ||
                string.IsNullOrWhiteSpace(userdto.password))
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Please provide valid first name, last name, and email.");
                return serviceResponse;
            }

            // Create instance of user
            Users user = new Users()
            {
                first_name = userdto.first_name,
                last_name = userdto.last_name,
                email = userdto.email,
                friend_list = userdto.friend_list,
                password = userdto.password
            };
            // Inserting into user 

            try
            {
                // Attempts to add user
                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("There was an error adding the Category.");
                serviceResponse.Messages.Add(ex.Message);

            }


            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = user.user_Id;
            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteUser(int id)
        {
            ServiceResponse response = new();

            var User = await _context.users.FindAsync(id);

            if (User == null)
            {

                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("User cannot be deleted because it does not exist.");
                return response;

            }

            try
            {
                //Attempts to delete user
                _context.users.Remove(User);
                await _context.SaveChangesAsync();

            }

            catch (Exception ex)
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add("Error encountered while deleting the User");
                return response;
            }

            response.Status = ServiceResponse.ServiceStatus.Deleted;

            return response;




        }
    }
}

