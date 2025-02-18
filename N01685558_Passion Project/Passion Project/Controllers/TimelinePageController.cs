using Microsoft.AspNetCore.Mvc;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passion_Project.Controllers
{
    public class TimelinePageController : Controller
    {
        private readonly ITimelineService _timelineService;
        private readonly IUserTimeline _userTimelineService;
        private readonly IEntriesService _entryService;  // Assuming you have an entry service to fetch entries

        // Dependency injection of Timeline service interface
        public TimelinePageController(ITimelineService timelineService, IUserTimeline userTimelineService, IEntriesService entryService)
        {
            _timelineService = timelineService;
            _userTimelineService = userTimelineService;
            _entryService = entryService;  // Inject EntryService to get entries
        }

        // GET: TimelinePage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<TimelineDto> timelineDtos = await _timelineService.List();
            return View(timelineDtos);
        }

        // GET: TimelinePage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Fetch the timeline by ID
            TimelineDto? timelineDto = await _timelineService.FindTimeline(id);

            // Fetch the users associated with this timeline
            IEnumerable<UserTimelineDto> associatedUsers = await _userTimelineService.GetUsersForTimeline(id);

            // Fetch the entries associated with this timeline
            IEnumerable<EntriesDto> associatedEntries = await _entryService.GetEntriesForTimeline(id);

            // Check if the timeline exists
            if (timelineDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find timeline"] });
            }
            else
            {
                // Prepare the TimelineDetails view model
                TimelineDetails timelineInfo = new TimelineDetails()
                {
                    Timeline = timelineDto,
                    UserTimeline = associatedUsers,
                    Entries = associatedEntries 
                };

                return View(timelineInfo);  // Pass TimelineDetails to the view
            }
        }

        public async Task<IActionResult> DetailsForUser(int id)
        {
            // Fetch the timeline by ID
            TimelineDto? timelineDto = await _timelineService.FindTimeline(id);

            // Fetch the users associated with this timeline
            IEnumerable<UserTimelineDto> associatedUsers = await _userTimelineService.GetUsersForTimeline(id);

            // Fetch the entries associated with this timeline
            IEnumerable<EntriesDto> associatedEntries = await _entryService.GetEntriesForTimeline(id);

            // Check if the timeline exists
            if (timelineDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find timeline"] });
            }
            else
            {
                // Prepare the TimelineDetails view model
                TimelineDetails timelineInfo = new TimelineDetails()
                {
                    Timeline = timelineDto,
                    UserTimeline = associatedUsers,
                    Entries = associatedEntries
                };

                return View(timelineInfo);  // Pass TimelineDetails to the view
            }
        }

        // GET: TimelinePage/New
        public IActionResult New()
        {
            return View();
        }

        public IActionResult AddTimeline()
        {
            return View();
        }

        // POST: TimelinePage/Add
        [HttpPost]
        public async Task<IActionResult> Add(TimelineDto timelineDto)
        {
            ServiceResponse response = await _timelineService.AddTimeline(timelineDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "TimelinePage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFromUser(TimelineDto timelineDto)
        {
            ServiceResponse response = await _timelineService.AddTimeline(timelineDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Dashboard", "UserPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: TimelinePage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            TimelineDto? timelineDto = await _timelineService.FindTimeline(id);
            if (timelineDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find timeline"] });
            }
            return View(timelineDto);  // Pass the timelineDto to the view
        }

        [HttpGet]
        public async Task<IActionResult> EditByUser(int id)
        {
            TimelineDto? timelineDto = await _timelineService.FindTimeline(id);
            if (timelineDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find timeline"] });
            }
            return View(timelineDto);  // Pass the timelineDto to the view
        }

        // POST: TimelinePage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, TimelineDto timelineDto)
        {
            ServiceResponse response = await _timelineService.UpdateTimeline(id, timelineDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "TimelinePage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateByUser(int id, TimelineDto timelineDto)
        {
            ServiceResponse response = await _timelineService.UpdateTimeline(id, timelineDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "TimelinePage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: TimelinePage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            TimelineDto? timelineDto = await _timelineService.FindTimeline(id);
            if (timelineDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find timeline"] });
            }
            return View(timelineDto);  // Pass timelineDto to the view
        }

        public async Task<IActionResult> UserConfirmDelete(int id)
        {
            TimelineDto? timelineDto = await _timelineService.FindTimeline(id);
            if (timelineDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find timeline"] });
            }
            return View(timelineDto);  // Pass timelineDto to the view
        }

        // POST: TimelinePage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _timelineService.DeleteTimeline(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "TimelinePage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserDelete(int id)
        {
            ServiceResponse response = await _timelineService.DeleteTimeline(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("Dashboard", "UserPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }
    }
}
