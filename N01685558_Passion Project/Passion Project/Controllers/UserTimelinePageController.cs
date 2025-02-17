using Microsoft.AspNetCore.Mvc;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passion_Project.Controllers
{
    public class UserTimelinePageController : Controller
    {
        private readonly IUserTimeline _userTimelineService;
        private readonly IUserService _userService;
        private readonly ITimelineService _timelineService;

        public UserTimelinePageController(IUserTimeline userTimelineService, IUserService userService, ITimelineService timelineService)
        {
            _userTimelineService = userTimelineService;
            _userService = userService;
            _timelineService = timelineService;
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

        [HttpPost]
        public async Task<IActionResult> UnlinkUserFromTimeline(int userId, int timelineId)
        {
            await _userTimelineService.UnlinkUserFromTimeline(userId, timelineId);
            return RedirectToAction("Index");
        }

        // GET: UserTimelinePage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.FindUser(id);
            var userTimelines = await _userTimelineService.GetTimelinesForUser(id);

            if (user == null)
            {
                return View("Error", new ErrorViewModel { Errors = new List<string> { "User not found" } });
            }

            var userDetails = new UserDetails
            {
                User = user,
                UserTimeline = userTimelines
            };

            return View(userDetails);
        }

        // GET: UserTimelinePage/Assign
        public IActionResult Assign()
        {
            return View();
        }

       
    }
}
