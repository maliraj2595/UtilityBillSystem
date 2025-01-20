using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using UtilityBill;
using UtilityBill.Data;

namespace UtilityBill.Controllers
{
    public class BillDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BillDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BillDetail.Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BillDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BillDetail == null)
            {
                return NotFound();
            }

            var billDetail = await _context.BillDetail
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BillDetailId == id);
            if (billDetail == null)
            {
                return NotFound();
            }

            return View(billDetail);
        }

        // GET: BillDetails/Create
        public IActionResult Create(int userId)
        {
            // Check if the user exists and doesn't have a bill associated
            //var user = _context.User
            //    .Include(u => u.BillDetails)
            //    .SingleOrDefault(u => u.UserId == userId && !u.BillDetails.Any());

            //if (user == null)
            //{
            //    return NotFound();
            //}
            // Get all users
            var allusers = _context.User.ToList();

            // Get all users who have a bill
            var usersWithBill = _context.BillDetail.Select(b => b.User).ToList();
            //var usersWithoutBill = _context.User.Where(u => u.BillDetails == null).ToList();

            // Get all users who don't have a bill
            var usersWithoutBill = allusers.Except(usersWithBill).ToList();

            // Pass users without bill to the view
            ViewBag.UsersWithoutBill = usersWithoutBill;

            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email");
            return View();
        }

        // POST: BillDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int userId,[Bind("BillDetailId,UnitsConsumed,TotalBill,Rate,BillDate,BillStatus,UserId")] BillDetail billDetail)
        {
            var user = await _context.User.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
               _context.Add(billDetail);
                await _context.SaveChangesAsync();
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", billDetail.UserId);
            return RedirectToAction(nameof(Index));
           
            
            //return View(billDetail);
        }

        // GET: BillDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BillDetail == null)
            {
                return NotFound();
            }

            var billDetail = await _context.BillDetail.FindAsync(id);
            if (billDetail == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", billDetail.UserId);
            return View(billDetail);
        }

        // POST: BillDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillDetailId,UnitsConsumed,TotalBill,Rate,BillDate,BillStatus,UserId")] BillDetail billDetail)
        {
            if (id != billDetail.BillDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillDetailExists(billDetail.BillDetailId))
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
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Email", billDetail.UserId);
            return View(billDetail);
        }

        // GET: BillDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BillDetail == null)
            {
                return NotFound();
            }

            var billDetail = await _context.BillDetail
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BillDetailId == id);
            if (billDetail == null)
            {
                return NotFound();
            }

            return View(billDetail);
        }

        // POST: BillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BillDetail == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BillDetail'  is null.");
            }
            var billDetail = await _context.BillDetail.FindAsync(id);
            if (billDetail != null)
            {
                _context.BillDetail.Remove(billDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillDetailExists(int id)
        {
          return (_context.BillDetail?.Any(e => e.BillDetailId == id)).GetValueOrDefault();
        }
    }
}
