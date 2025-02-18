using Microsoft.AspNetCore.Mvc;
using Passion_Project.Interfaces;
using Passion_Project.Models;
using Passion_Project.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passion_Project.Controllers
{
    public class UserPageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserTimeline _userTimelineService;
        private readonly IEntriesService _entryService; 

        // Dependency injection of service interface
        public UserPageController(IUserService userService, IUserTimeline userTimelineService, IEntriesService entryService)
        {
            _userService = userService;
            _userTimelineService = userTimelineService;
            _entryService = entryService;
        }

        // GET: UserPage/UserLogin
        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
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
       

        public IActionResult UserRegister()
        {
            return View();
        }

       

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Link()
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

      



        // POST: UserPage/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _userService.ValidateUser(email, password);

            if (result.Status == ServiceResponse.ServiceStatus.Success)
            {
                // Save user email in session after successful login
                HttpContext.Session.SetString("UserEmail", email);

                // Redirect to User Dashboard
                return RedirectToAction("Dashboard", "UserPage");
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid email or password.";
                return View("UserLogin"); // Stay on the login page and show error
            }
        }

        // GET: UserPage/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");

            if (userEmail == null)
            {
                return RedirectToAction("UserLogin", "UserPage"); // Redirect to login if no session exists
            }

            // Retrieve user information based on session email
            var userResponse = await _userService.GetUserByEmail(userEmail); // Fetch user by email

            if (userResponse.Status == ServiceResponse.ServiceStatus.Error)
            {
                return RedirectToAction("UserLogin", "UserPage"); // If user not found, redirect to login page
            }

            var user = userResponse.Data; // Extract the UserDto from the response

            // Retrieve user's timelines and entries for dashboard overview
            IEnumerable<UserTimelineDto> associatedTimelines = await _userTimelineService.GetTimelinesForUser(user.user_Id);
            IEnumerable<EntriesDto> recentEntries = await _entryService.GetRecentEntries(user.user_Id); // Now calls GetRecentEntries

            // Create a dashboard view model to pass to the view
            var dashboardInfo = new DashboardViewModel
            {
                User = user,
                UserTimelines = associatedTimelines,
                RecentEntries = recentEntries
            };

            return View(dashboardInfo);
        }

        

    }
}
