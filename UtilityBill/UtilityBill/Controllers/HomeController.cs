using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UtilityBill.Data;
using UtilityBill.Models;
using UtilityBill.Models.ViewModels;

namespace UtilityBill.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = _context.User.SingleOrDefault(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));

                if (user != null)
                {
                    // Authentication successful, store the role in TempData
                    HttpContext.Session.SetString("UserRole", user.Role.ToString());

                    // Authentication successful, create a session or authentication cookie to persist the user's login state.
                    // For example, you can use ASP.NET Core Identity to manage user authentication and session management.
                    // For demonstration purposes, we'll use simple session-based authentication.
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("Role", user.Role.ToString());

                    if (user.Role.Equals(Role.ADMIN))
                    {

                        // Redirect to the Index action of the Admin controller.
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        // Redirect to a Index action of the User controller.
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password. Please try again.");
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Email,
                    Email = model.Email,
                    Password = model.Password,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    MobileNumber = model.MobileNumber,
                    Role = Role.USER,
                    Address = new Address
                    {
                        Street = model.Address.Street,
                        City = model.Address.City,
                        PinCode = model.Address.PinCode,
                        State = model.Address.State
                    },
                    ApplicationDetail = new ApplicationDetail
                    {
                        ConnectionType = model.ApplicationDetail.ConnectionType,
                        RequiredLoad = model.ApplicationDetail.RequiredLoad,
                        ApplicationStatus = "NEW",
                        ApplicationDate = DateTime.Now,
                        MeterDetail = new MeterDetail
                        {
                            Status = "INACTIVE",
                            InstallationDate = DateTime.Now
                        }
                    }
                };
                _context.User.Add(user);
                _context.SaveChanges();
                // Redirect to a success page or any other desired page
                return RedirectToAction("login");
            };

                return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}