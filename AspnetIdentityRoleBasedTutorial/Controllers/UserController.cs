using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspnetIdentityRoleBasedTutorial.Data;
using AspnetIdentityRoleBasedTutorial.Models;

namespace AspnetIdentityRoleBasedTutorial.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        // GET: /User
        public async Task<IActionResult> Index()
        {
            // Get all users except the currently logged-in admin
            var users = await _userManager.Users
                .Where(u => u.Email != User.Identity.Name)
                .ToListAsync();

            return View(users);
        }

        // GET: /User/Delete
        public async Task<IActionResult> Delete(string email)
        {
            if (email == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: /User/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                // Delete the user using UserManager
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    // Optionally, you can handle a successful delete
                    // For example, display a success message
                }
                else
                {
                    // Handle errors if the delete operation fails
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return RedirectToAction("Index");
        }


        // GET: /User/DeleteConfirmed
        public IActionResult DeleteConfirmed()
        {
            return View(); // Display the confirmation view
        }
    }
}
