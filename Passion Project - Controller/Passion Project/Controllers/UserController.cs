using Microsoft.AspNetCore.Mvc;
using Passion_Project.Data;
using Passion_Project.Models;
using Microsoft.EntityFrameworkCore;
using Passion_Project.Data.Migrations;

namespace Passion_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {


        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {

            _context = context;
        }

        // GET : api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.users.ToListAsync();
        }

        // GET api/User/{id}

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {

            var user = await _context.users.FindAsync(id);

            if (user == null) {

                return NotFound();
            }

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.user_Id == id);
        }

        // api/Users/{id}

        [HttpPut("{id}")]

        public async Task<IActionResult> PutUser(int id, Users user)
        {

            if (id != user.user_Id)
            {

                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {

                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            
            }

            return NoContent(); 
        }

        [HttpPost]

        public async Task<ActionResult<Users>> PostUser(Users product)
        {
            _context.users.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = product.user_Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var product = await _context.users.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.users.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
