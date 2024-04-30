using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace VSAtelier.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole viewModel)
        {
            //!duplikacji
                if(!await roleManager.RoleExistsAsync(viewModel.Name!))
            {
              var role = await roleManager.CreateAsync(new IdentityRole(viewModel.Name!));
              if (role.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    return BadRequest("Nie udało się utowrzyć roli");
                }
            }
            else
            {
                return BadRequest("Rola już istnieje");
            }

            
        }
    }
}
