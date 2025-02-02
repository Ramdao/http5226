using Microsoft.AspNetCore.Mvc;
using Passion_Project.Data;
using Passion_Project.Models;
using Microsoft.EntityFrameworkCore;
using Passion_Project.Data.Migrations;
using Passion_Project.Interfaces;
using Azure;



using Microsoft.AspNetCore.Mvc;
using Passion_Project.Services;

namespace Passion_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTimelineController : Controller
    {

        private readonly IUserTimeline _context;

        public UserTimelineController(IUserTimeline context)
        {

            _context = context;
        }
        /// <summary>
        /// Return a list of User Timelines.
        /// </summary>
        /// <returns>
        /// 200 OK
        /// [{UserTimelineDto}, {UserTimelineDto}, ...]
        /// </returns>
        /// <example>
        /// GET : api/UserTimeline/ListUserTimeline -> [{ {UserTimelineDto}, {UserTimelineDto}, ... }]
        /// </example>
        [HttpGet(template: "ListUserTimeline")]
        public async Task<ActionResult<IEnumerable<UserTimelineDto>>> GetUserTimeline()
        {
            IEnumerable<UserTimelineDto> UserTimeline = await _context.List();
            return Ok(UserTimeline);
        }
        /// <summary>
        /// Links a user to a timeline.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="timelineId">The ID of the timeline.</param>
        /// <returns>
        /// 204 No Content - If the user is successfully linked to the timeline.
        /// 404 Not Found - If either the user or timeline does not exist.
        /// 409 Conflict - If the user is already linked to the timeline.
        /// 500 Internal Server Error - If an error occurs while processing.
        /// </returns>
        /// <example>
        /// POST : api/UserTimeline/LinkUserToTimeline?userId=1&timelineId=2 -> 204 No Content
        /// </example>

        [HttpPost("LinkUserToTimeline")]
        public async Task<ActionResult> LinkUserToTimeline(int userId, int timelineId)
        {
            ServiceResponse response = await _context.LinkUserToTimeline(userId, timelineId);

            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return Conflict(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }

            return NoContent();
        }
        /// <summary>
        /// Unlinks a user from a timeline.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="timelineId">The ID of the timeline.</param>
        /// <returns>
        /// 204 No Content - If the user is successfully unlinked from the timeline.
        /// 404 Not Found - If the user is not linked to the specified timeline.
        /// 500 Internal Server Error - If an error occurs while processing.
        /// </returns>
        /// <example>
        /// DELETE : api/UserTimeline/UnlinkUserFromTimeline?userId=1&timelineId=2 -> 204 No Content
        /// </example>

        [HttpDelete("UnlinkUserFromTimeline")]
        public async Task<ActionResult> UnlinkUserFromTimeline(int userId, int timelineId)
        {
            ServiceResponse response = await _context.UnlinkUserFromTimeline(userId, timelineId);

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



    }
}
