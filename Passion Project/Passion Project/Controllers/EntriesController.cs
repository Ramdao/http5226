using Microsoft.AspNetCore.Mvc;
using Passion_Project.Data;
using Passion_Project.Models;
using Microsoft.EntityFrameworkCore;
using Passion_Project.Interfaces;

namespace Passion_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : Controller
    {
        private readonly IEntriesService _context;

        public EntriesController(IEntriesService context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all entries
        /// </summary>
        /// <returns>
        /// 200 OK
        /// [{EntriesDto}, {EntriesDto}, ...]
        /// </returns>
        /// <example>
        /// GET: api/Entries/ListEntries -> [{EntriesDto}, {EntriesDto}, ...]
        /// </example>
        [HttpGet(template: "ListEntries")]
        public async Task<ActionResult<IEnumerable<EntriesDto>>> GetEntries()
        {
            IEnumerable<EntriesDto> EntriesDtos = await _context.List();
            return Ok(EntriesDtos);
        }

        /// <summary>
        /// Returns a single entry by {id}
        /// </summary>
        /// <param name="id">The entry id</param>
        /// <returns>
        /// 200 OK
        /// {EntriesDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Entries/Find/1 -> {EntriesDto}
        /// </example>
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<EntriesDto>> FindEntry(int id)
        {
            var entry = await _context.FindEntry(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        /// <summary>
        /// Updates an existing entry
        /// </summary>
        /// <param name="id">ID of the entry to update</param>
        /// <param name="entryDto">Updated entry data</param>
        /// <returns>
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// or
        /// 204 No Content
        /// </returns>
        /// <example>
        /// PUT: api/Entries/Update/3
        /// Request Body: {EntriesDto}
        /// </example>
        [HttpPut(template: "Update/{id}")]
        public async Task<IActionResult> PutEntry(int id, EntriesDto entryDto)
        {
            var response = await _context.UpdateEntry(id, entryDto);
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
        /// Adds a new entry
        /// </summary>
        /// <param name="entryDto">Data for the new entry</param>
        /// <returns>
        /// 201 Created
        /// Location: api/Entries/Find/{EntryId}
        /// {EntriesDto}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// POST: api/Entries/Add
        /// Request Body: {EntriesDto}
        /// -> Response Code: 201 Created
        /// </example>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<EntriesDto>> PostEntry(EntriesDto entryDto)
        {
            ServiceResponse response = await _context.AddEntry(entryDto);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Created($"api/Entries/Find/{response.CreatedId}", entryDto);
        }

        /// <summary>
        /// Deletes an entry by ID
        /// </summary>
        /// <param name="id">ID of the entry to delete</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// DELETE: api/Entries/Delete/4
        /// -> Response: 204 No Content
        /// </example>
        [HttpDelete(template: "Delete/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            ServiceResponse response = await _context.DeleteEntry(id);
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
