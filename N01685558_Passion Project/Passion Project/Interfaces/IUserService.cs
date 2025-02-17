using Microsoft.AspNetCore.Identity;
using Passion_Project.Models;

namespace Passion_Project.Interfaces

{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> List();

        Task<UserDto?> FindUser(int id);

        Task<ServiceResponse>  UpdateUser(int id, UserDto userDto); 

        Task<ServiceResponse> AddUser(UserDto userDto);

        Task<ServiceResponse> DeleteUser(int id);

        Task<ServiceResponse> ValidateUser(string email, string password);

        Task<ServiceResponse<UserDto>> GetUserByEmail(string email);
    }
}

