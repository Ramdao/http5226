using Microsoft.AspNetCore.Mvc;
using Passion_Project.Data;
using Passion_Project.Models;
using Microsoft.EntityFrameworkCore;
using Passion_Project.Data.Migrations;
using Passion_Project.Interfaces;
using Azure;


namespace Passion_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {


        private readonly IUserService _context;

        // dependency injection of service interfaces
        public UserController(IUserService context)
        {

            _context = context;
        }

        /// <summary>
        /// Return a list of Users
        /// </summary>
        /// <return>
        /// 200 ok
        /// [{UserDto}, {UserDto} , ...]
        /// </return>
        /// <example>
        /// GET : api/User/List -> [{ {UserDto}, {UserDto}, ... ]}
        /// </example>
        [HttpGet(template:"ListUser")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            IEnumerable<UserDto> UserDtos = await _context.List();
            return Ok(UserDtos);
        }

        /// <summary>
        /// Returns a single user by {id}
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>
        /// 200 OK
        /// {UserDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/User/Find/1 -> {UserDto}
        /// </example>

        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<UserDto>> FindUser(int id)
        {

            var user = await _context.FindUser(id);

            if (user == null)
            {

                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> id to update user</param>
        /// <param name="userdto">getting exiting data</param>
        /// <returns>
        /// 400 Bad request
        /// or
        /// 404 not found
        /// or
        /// 204 No content
        /// </returns>
        /// <example>
        /// PUT : api/Category/Update/3
        /// [{userdto}]
        /// -> server: Kestrel 
        /// </example>

        [HttpPut(template: "Update/{id}")]

        public async Task<IActionResult> PutUser(int id, UserDto userdto)
        {
            var response = await _context.UpdateUser(id, userdto);

            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userdto"> required data to enter new user</param>
        /// <returns>
        /// 201 Created
        /// Location: api/User/FindUser/{UserId}
        /// {UserDto}
        /// or
        /// 404 not found
        /// </returns>
        /// <example>
        /// POST: api/User/add
        /// Request Body: {UserDto}
        /// ->
        /// Response Code: 201 Created
        /// </example>

        [HttpPost(template:"Add")]

        public async Task<ActionResult<Users>> PostUser(UserDto userdto)
        {
            ServiceResponse response = await _context.AddUser(userdto);

            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return Created($"api/User/FindUser/{response.CreatedId}", userdto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id to delete the user</param>
        /// <returns>
        /// 204 No content
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// DELETE: api/Delete/4
        /// -> server: Kestrel 
        /// </example>


        [HttpDelete(template:"Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            ServiceResponse response = await _context.DeleteUser(id);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound();
            }

            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return NoContent();
        }


    }
}
