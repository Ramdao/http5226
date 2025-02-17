using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Passion_Project.Data;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passion_Project.Controllers
{
    public class UserTimelinePageController : Controller
    {
        private readonly IUserTimeline _userTimelineService;
        private readonly IUserService _userService;
        private readonly ITimelineService _timelineService;
        private readonly ApplicationDbContext _context;  // Inject ApplicationDbContext

        public UserTimelinePageController(IUserTimeline userTimelineService, IUserService userService, ITimelineService timelineService, ApplicationDbContext context)
        {
            _userTimelineService = userTimelineService;
            _userService = userService;
            _timelineService = timelineService;
            _context = context;  // Assign ApplicationDbContext
        }

        // GET: UserTimelinePage/Index
        public async Task<IActionResult> Index()
        {
            var users = await _userService.List();
            var timelines = await _timelineService.List();

            var viewModel = new UserTimelineViewModel
            {
                Users = users,
                Timelines = timelines
            };

            return View(viewModel);
        }

        // POST: UserTimelinePage/LinkUserToTimeline
        [HttpPost]
        public async Task<IActionResult> LinkUserToTimeline(int userId, int timelineId)
        {
            await _userTimelineService.LinkUserToTimeline(userId, timelineId);
            return RedirectToAction("Index");
        }

        // POST: UserTimelinePage/UnlinkUserFromTimeline
        [HttpPost]
        public async Task<IActionResult> UnlinkUserFromTimeline(int userId, int timelineId)
        {
            await _userTimelineService.UnlinkUserFromTimeline(userId, timelineId);
            return RedirectToAction("Index");
        }

        // GET: UserTimelinePage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> GetRecentEntries(int userId)
        {
            // Retrieve the timelines for the user
            var timelines = await _userTimelineService.GetTimelinesForUser(userId);
            var timelineIds = timelines.Select(t => t.timeline_Id).ToList();

            // Get the recent entries for the user, filtering by timeline_Id
            var entries = await _context.entries
                                        .Where(e => timelineIds.Contains(e.timeline_Id))  // Filter entries by timeline_Id
                                        .OrderByDescending(e => e.entries_Id)             // Or order by a different field like entry_date if needed
                                        .Take(5)                                           // Limit the number of entries
                                        .ToListAsync();

            // Map entries to EntriesDto
            List<EntriesDto> entryDtos = new List<EntriesDto>();

            foreach (var entry in entries)
            {
                entryDtos.Add(new EntriesDto
                {
                    entries_Id = entry.entries_Id,
                    entries_name = entry.entries_name,
                    description = entry.description,
                    location = entry.location,
                    images = entry.images,
                    timeline_Id = entry.timeline_Id,
                    Timeline = new TimelineDto()  // Assuming TimelineDto exists to map the related Timeline
                    {
                        timeline_Id = entry.Timeline.timeline_Id,
                        // Add other properties from TimelineDto if needed
                    }
                });
            }

            // Return as partial view or redirect to a view that displays the entries
            return PartialView("_RecentEntries", entryDtos);  // Assuming you have a partial view "_RecentEntries.cshtml"
        }

        // GET: UserTimelinePage/Assign
        public IActionResult Assign()
        {
            return View();
        }
    }
}
