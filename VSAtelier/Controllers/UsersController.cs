using Microsoft.AspNetCore.Mvc;
using VSAtelier.Data;
using VSAtelier.Models.Entities;
using Microsoft.EntityFrameworkCore;
using VSAtelier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace VSAtelier.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        //private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = viewModel.UserName,
                    name = viewModel.name,
                    surname = viewModel.surname,
                    Email = viewModel.Email,
                    address = viewModel.address,
                };
               var result = await userManager.CreateAsync(user, viewModel.PasswordHash!);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("ListUsers", "Users");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var roles = await roleManager.Roles.ToListAsync();

            ViewBag.UsersWithRoles = new Dictionary<string, IList<string>>();

            foreach (var user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                ViewBag.UsersWithRoles[user.Id] = userRoles;
            }

            ViewBag.Roles = roles.Select(x => x.Name);

            return View(users);

        }
        [HttpGet]
        public async Task<IActionResult> EditUsers(string id)
        {
            var roles = await roleManager.Roles.ToListAsync();
            ViewBag.RoleList = roles.Select(x => x.Name);
            var users = await userManager.FindByIdAsync(id);
            var userRoles = await userManager.GetRolesAsync(users);
            ViewBag.CurrentUserRole = userRoles.FirstOrDefault();

            return View(users);
        }
            [HttpPost]
            public async Task<IActionResult> EditUsers(User viewModel, string role)
            {

                var users = await userManager.FindByIdAsync(viewModel.Id);
                if (users is not null)
                {
                    users.name = viewModel.name;
                    users.surname = viewModel.surname;
                    users.Email = viewModel.Email;
                    users.PhoneNumber = viewModel.PhoneNumber;

                    await userManager.UpdateAsync(users);

                    var userRoles = await userManager.GetRolesAsync(users);

                    foreach (var userRole in userRoles)
                    {
                        await userManager.RemoveFromRoleAsync(users, userRole);
                    }

                    var selectedRole = await roleManager.FindByNameAsync(role);
                
                        if (selectedRole != null)
                        {
                            await userManager.AddToRoleAsync(users, selectedRole.Name!);
                        }
                
                }
                
                return RedirectToAction("ListUsers", "Users");

            }
        [HttpPost]
        public async Task<IActionResult> DeleteUsers(User viewModel)
        {
            var users = await userManager.FindByIdAsync(viewModel.Id);
            if (users != null)
            {
                var result = await userManager.DeleteAsync(users);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Users");
                }
                else
                {
                    //
                }
            }
            return NotFound();
        }
    }
}
