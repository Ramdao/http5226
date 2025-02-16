using Microsoft.AspNetCore.Mvc;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passion_Project.Controllers
{
    public class EntryPageController : Controller
    {
        private readonly IEntriesService _entriesService;
        private readonly ITimelineService _timelineService;

        // Dependency injection of service interfaces
        public EntryPageController(IEntriesService entriesService, ITimelineService timelineService)
        {
            _entriesService = entriesService;
            _timelineService = timelineService;
        }

        // Redirect to List action
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: EntryPage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<EntriesDto> entriesDtos = await _entriesService.List();
            return View(entriesDtos);
        }

        // GET: EntryPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            EntriesDto? entriesDto = await _entriesService.FindEntry(id);

            if (entriesDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = new List<string> { "Could not find entry" } });
            }
            else
            {
                return View(entriesDto);  // Pass the EntriesDto to the view
            }
        }

        // GET: EntryPage/New
        public async Task<IActionResult> New()
        {
            // Fetch available timelines to associate with the new entry
            var timelines = await _timelineService.List(); // Assuming a method to list all timelines
            ViewBag.Timelines = timelines;
            return View();
        }

        // POST: EntryPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(EntriesDto entriesDto)
        {
            ServiceResponse response = await _entriesService.AddEntry(entriesDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "EntryPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: EntryPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EntriesDto? entriesDto = await _entriesService.FindEntry(id);
            if (entriesDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = new List<string> { "Could not find entry" } });
            }
            else
            {
                // Fetch available timelines for updating entry association
                var timelines = await _timelineService.List();
                ViewBag.Timelines = timelines;
                return View(entriesDto);
            }
        }

        // POST: EntryPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, EntriesDto entriesDto)
        {
            ServiceResponse response = await _entriesService.UpdateEntry(id, entriesDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "EntryPage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: EntryPage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            EntriesDto? entriesDto = await _entriesService.FindEntry(id);
            if (entriesDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = new List<string> { "Could not find entry" } });
            }
            else
            {
                return View(entriesDto);
            }
        }

        // POST: EntryPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _entriesService.DeleteEntry(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "EntryPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }
    }
}
