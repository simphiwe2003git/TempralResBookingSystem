using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetIdentityRoleBasedTutorial.Data;
using AspnetIdentityRoleBasedTutorial.Models.BookRoomModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Policy;

namespace AspnetIdentityRoleBasedTutorial.Controllers
{
    [Authorize]
    public class RoomBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomBookings
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                // If the user is an admin, show all bookings
                return View(await _context.RoomBooking.ToListAsync());
            }

            // For regular users, show only their own bookings
            var userEmail = User.Identity.Name;
            var userBookings = await _context.RoomBooking
                .Where(b => b.Email == userEmail)
                .ToListAsync();

            return View(userBookings);
        }


        // GET: RoomBookings/Create
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Admin"))
            {
                // Redirect admin to the access denied page
                return RedirectToAction("Index");
            }

            // Check if the user already has a booking
            var userEmail = User.Identity.Name;
            var userHasBooking = await _context.RoomBooking
                .AnyAsync(b => b.Email == userEmail);

            if (userHasBooking)
            {
                // Redirect users with existing bookings to the index page to view bookings
                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("Id,FirstName,LastName,Email,Province,City,RoomType,BlockName,CheckInDate,CheckOutDate")] RoomBooking roomBooking)
        {



            if (ModelState.IsValid)
            {
                _context.Add(roomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomBooking);
        }
        // GET: RoomBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomBooking == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBooking.FindAsync(id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            if (User.Identity.Name != roomBooking.Email)
            {
                // If the logged-in user is not the owner, deny access or handle it as per your application's design.
                return RedirectToAction("AccessDenied", "Home"); // Redirect to an access denied page or handle it accordingly.
            }

            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Province,City,RoomType,BlockName,CheckInDate,CheckOutDate")] RoomBooking roomBooking)
        {
            if (id != roomBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (User.Identity.Name != roomBooking.Email)
                {
                    // If the logged-in user is not the owner, deny access
                    return RedirectToAction("AccessDenied", "Home"); // Redirect to an access denied page or handle it as per your application's design.
                }

                try
                {
                    _context.Update(roomBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomBookingExists(roomBooking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roomBooking);
        }




        // GET: RoomBookings/Delete/5
        [Authorize(Roles = "Admin,User")] // Allow both admin and user

        public async Task<IActionResult>
            Delete(int? id)
        {
            if (id == null || _context.RoomBooking == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBooking
            .FirstOrDefaultAsync(m => m.Id == id);
            if (roomBooking == null)
            {
                return NotFound();
            }
            // Check if the logged-in user is the owner of the booking or is an admin
            if (User.Identity.Name != roomBooking.Email && !User.IsInRole("Admin"))
            {
                // If the logged-in user is not the owner and not an admin, deny access
                return RedirectToAction("AccessDenied", "Home"); // Redirect to an access denied page or handle it as per your application's design.
            }

            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            DeleteConfirmed(int id)
        {
            if (_context.RoomBooking == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoomBooking'  is null.");
            }
            var roomBooking = await _context.RoomBooking.FindAsync(id);
            if (roomBooking != null)
            {
                _context.RoomBooking.Remove(roomBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomBookingExists(int id)
        {
            return (_context.RoomBooking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Status(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBooking.FindAsync(id);

            if (roomBooking == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                return View("EditStatus", roomBooking);
            }
            else if (User.Identity.Name != roomBooking.Email)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View("Status", roomBooking);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult EditStatus(int id, string bookingStatus)
        {
            var roomBooking = _context.RoomBooking.Find(id);

            if (roomBooking != null)
            {
                if (Enum.TryParse(bookingStatus, out BookingStatus status))
                {
                    roomBooking.BookingStatus = status;
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });

        }

    }
}
