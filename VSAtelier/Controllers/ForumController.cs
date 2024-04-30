using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VSAtelier.Data;
using VSAtelier.Models;
using VSAtelier.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace VSAtelier.Controllers
{
    public class ForumController : Controller
    {
        private readonly ForumDbContext _context;
        private readonly ILogger<ForumController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ForumController(ForumDbContext context,ILogger<ForumController> loggerContext,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = loggerContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Forum()
        {
            var items = _context.Forums.ToList();

            var viewModel = new Forum
            {
                Forums = items,
                NewForumOG = new Forum()
            };
             
           
            return View(viewModel);
        }
        [HttpPost]
        public  IActionResult Forum(ForumVM file)
        {
            string userName = HttpContext.User.Identity.Name;
            string userRole = "Użytkownik";
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole("Admin"))
                {
                    userRole = "Administrator";
                }
                else if (HttpContext.User.IsInRole("Mod"))
                {
                    userRole = "Moderator";
                }
            }
            string stringFileName = UploadFile(file);
            var comment = new Forum
            {
                textForm = file.textForm,
                imagePhoto = stringFileName,
                Name = userName,
                Role = userRole
            };
            _context.Forums.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Forum");
        }

        private string UploadFile(ForumVM file)
        {
            string fileName = null;
            if (file.imagePhoto != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString()+"-"+ file.imagePhoto.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.imagePhoto.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    }
}
