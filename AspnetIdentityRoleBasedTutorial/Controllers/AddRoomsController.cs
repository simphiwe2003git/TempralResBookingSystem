using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetIdentityRoleBasedTutorial.Data;
using AspnetIdentityRoleBasedTutorial.Models.ManageRooms;
using Microsoft.AspNetCore.Authorization;

namespace AspnetIdentityRoleBasedTutorial.Controllers
{
    [Authorize]
    public class AddRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddRooms
        public async Task<IActionResult> Index()
        {
              return _context.AddRooms != null ? 
                          View(await _context.AddRooms.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AddRooms'  is null.");
        }

        // GET: AddRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddRooms == null)
            {
                return NotFound();
            }

            var addRooms = await _context.AddRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addRooms == null)
            {
                return NotFound();
            }

            return View(addRooms);
        }

        // GET: AddRooms/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AddRoomNumber,AddRoomType,AddBlockName,AddRoomSatus")] AddRooms addRooms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addRooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addRooms);
        }

        // GET: AddRooms/Edit/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddRooms == null)
            {
                return NotFound();
            }

            var addRooms = await _context.AddRooms.FindAsync(id);
            if (addRooms == null)
            {
                return NotFound();
            }
            return View(addRooms);
        }

        // POST: AddRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AddRoomNumber,AddRoomType,AddBlockName,AddRoomSatus")] AddRooms addRooms)
        {
            if (id != addRooms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addRooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddRoomsExists(addRooms.Id))
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
            return View(addRooms);
        }
        [Authorize(Roles = "Admin")]

        // GET: AddRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddRooms == null)
            {
                return NotFound();
            }

            var addRooms = await _context.AddRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addRooms == null)
            {
                return NotFound();
            }

            return View(addRooms);
        }

        // POST: AddRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddRooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AddRooms'  is null.");
            }
            var addRooms = await _context.AddRooms.FindAsync(id);
            if (addRooms != null)
            {
                _context.AddRooms.Remove(addRooms);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddRoomsExists(int id)
        {
          return (_context.AddRooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
