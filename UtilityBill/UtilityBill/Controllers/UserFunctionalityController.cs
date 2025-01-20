using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UtilityBill;
using UtilityBill.Data;

namespace UtilityBill.Controllers
{
    public class UserFunctionalityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFunctionalityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the logged-in user's ID from the session
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            // Retrieve the user from the database
            var user = _context.User.Find(userId);

            if (user == null)
            {
                return NotFound();
            }
            var applicationDetail = _context.User
                .Include(u => u.ApplicationDetail)
                .FirstOrDefault(u => u.UserId == user.UserId)?
                .ApplicationDetail;

            // Get the meter detail associated with the user
            // Fetch application details associated with the user
            //var applicationDetail = _context.ApplicationDetail.FirstOrDefault(ad => ad.ApplicationDetailId == user.ApplicationDetailId);
            Console.WriteLine(applicationDetail);
            // If there are no application details for the user, set the status to "No Application"
            var applicationStatus = applicationDetail?.ApplicationStatus ?? "No Application";


            // Fetch bills associated with the user using the UserId property
            var bills = _context.BillDetail
                .Where(b => b.UserId == userId)
                .ToList();

            // Pass the user, meterDetail, and bills to the view
            var model = new Tuple<User, string, List<BillDetail>>(user, applicationStatus, bills);

            return View(model);
        }

        [HttpPost]
        public IActionResult PayBill(int billId)
        {
            // Retrieve the bill from the database
            var bill = _context.BillDetail.Find(billId);

            if (bill != null)
            {
                // Update the bill status to "Paid"
                bill.BillStatus = BillStatus.PAID;
                _context.SaveChanges();
            }

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            // Retrieve the user from the database
            var user = _context.User.Find(userId);

            if (user == null)
            {
                return NotFound();
            }

            // Redirect back to the ElectricityBill action
            //return RedirectToAction("ElectricityBill");
            return View();
        }



        // GET: UserFunctionality/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UserFunctionality/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserFunctionality/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Email,Password,Gender,DateOfBirth,MobileNumber,Role,IsDeleted")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: UserFunctionality/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserFunctionality/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Email,Password,Gender,DateOfBirth,MobileNumber,Role,IsDeleted")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: UserFunctionality/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserFunctionality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'ApplicationDbContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
