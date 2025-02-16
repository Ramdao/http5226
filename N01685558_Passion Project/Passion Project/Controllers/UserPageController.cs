using Azure;
using Microsoft.AspNetCore.Mvc;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passion_Project.Controllers
{
    public class UserPageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserTimeline _userTimelineService;

        // Dependency injection of service interface
        public UserPageController(IUserService userService, IUserTimeline userTimelineService)
        {
            _userService = userService;
            _userTimelineService = userTimelineService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: UserPage/List
        public async Task<IActionResult> List()
        {
            IEnumerable<UserDto?> userDtos = await _userService.List();
            return View(userDtos);
        }

        // GET: UserPage/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            UserDto? userDto = await _userService.FindUser(id);
            IEnumerable<UserTimelineDto> AssociatedTimelines = await _userTimelineService.GetTimelinesForUser(id);

            if (userDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find user"] });
            }
            else
            {
                UserDetails UserInfo = new UserDetails()
                {
                    User = userDto,
                    UserTimeline = AssociatedTimelines
                };
                return View(UserInfo);  // Pass UserDetails to the view
            }
        }

        // GET: UserPage/New
        public IActionResult New()
        {
            return View();
        }

        // POST: UserPage/Add
        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            ServiceResponse response = await _userService.AddUser(userDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("Details", "UserPage", new { id = response.CreatedId });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: UserPage/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            UserDto? userDto = await _userService.FindUser(id);
            if (userDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find user"] });
            }
            else
            {
                return View(userDto);
            }
        }

        // POST: UserPage/Update/{id}
        [HttpPost]
        public async Task<IActionResult> Update(int id, UserDto userDto)
        {
            ServiceResponse response = await _userService.UpdateUser(id, userDto); // Pass the id

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("Details", "UserPage", new { id = id });
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }

        // GET: UserPage/ConfirmDelete/{id}
        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            UserDto? userDto = await _userService.FindUser(id);
            if (userDto == null)
            {
                return View("Error", new ErrorViewModel() { Errors = ["Could not find user"] });
            }
            else
            {
                return View(userDto);
            }
        }

        // POST: UserPage/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _userService.DeleteUser(id);

            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("List", "UserPage");
            }
            else
            {
                return View("Error", new ErrorViewModel() { Errors = response.Messages });
            }
        }
    }
}
