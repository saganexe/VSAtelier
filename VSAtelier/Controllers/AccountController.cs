using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.Threading.Tasks;
using VSAtelier.Models;
using VSAtelier.Models.Entities;

namespace VSAtelier.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(viewModel.username);
                if (user != null && BCrypt.Net.BCrypt.Verify(viewModel.password, user.PasswordHash))
                {
                    await signInManager.SignInAsync(user, viewModel.rememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Zły login lub hasło.");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = viewModel.username,
                    name = viewModel.name,
                    surname = viewModel.surname,
                    Email = viewModel.email,
                    PhoneNumber = viewModel.PhoneNumber,
                    address = viewModel.address
                };

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(viewModel.password);

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Member");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}