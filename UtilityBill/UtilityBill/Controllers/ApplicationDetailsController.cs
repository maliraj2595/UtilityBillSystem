using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UtilityBill;
using UtilityBill.Data;

namespace UtilityBill.Controllers
{
    public class ApplicationDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationDetails
        public async Task<IActionResult> Index()
        {
              return _context.ApplicationDetail != null ? 
                          View(await _context.ApplicationDetail.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ApplicationDetail'  is null.");
        }

        // GET: ApplicationDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplicationDetail == null)
            {
                return NotFound();
            }

            var applicationDetail = await _context.ApplicationDetail
                .FirstOrDefaultAsync(m => m.ApplicationDetailId == id);
            if (applicationDetail == null)
            {
                return NotFound();
            }

            return View(applicationDetail);
        }

        // GET: ApplicationDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationDetailId,ApplicationDate,ApplicationStatus,ConnectionType,RequiredLoad")] ApplicationDetail applicationDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationDetail);
        }

        // GET: ApplicationDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplicationDetail == null)
            {
                return NotFound();
            }

            var applicationDetail = await _context.ApplicationDetail.FindAsync(id);
            if (applicationDetail == null)
            {
                return NotFound();
            }
            return View(applicationDetail);
        }

        // POST: ApplicationDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationDetailId,ApplicationDate,ApplicationStatus,ConnectionType,RequiredLoad")] ApplicationDetail applicationDetail)
        {
            if (id != applicationDetail.ApplicationDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationDetailExists(applicationDetail.ApplicationDetailId))
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
            return View(applicationDetail);
        }

        // GET: ApplicationDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicationDetail == null)
            {
                return NotFound();
            }

            var applicationDetail = await _context.ApplicationDetail
                .FirstOrDefaultAsync(m => m.ApplicationDetailId == id);
            if (applicationDetail == null)
            {
                return NotFound();
            }

            return View(applicationDetail);
        }

        // POST: ApplicationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicationDetail == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplicationDetail'  is null.");
            }
            var applicationDetail = await _context.ApplicationDetail.FindAsync(id);
            if (applicationDetail != null)
            {
                _context.ApplicationDetail.Remove(applicationDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationDetailExists(int id)
        {
          return (_context.ApplicationDetail?.Any(e => e.ApplicationDetailId == id)).GetValueOrDefault();
        }
    }
}
