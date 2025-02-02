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
    }
}
