using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectionVote;

namespace ElectionVote.Controllers
{
    public class CentreElectionsController : Controller
    {
        private readonly ELECTIONDBnewContext _context;

        public CentreElectionsController(ELECTIONDBnewContext context)
        {
            _context = context;
        }

        // GET: CentreElections
        public async Task<IActionResult> Index()
        {
            var eLECTIONDBnewContext = _context.CentreElections.Include(c => c.Administrateur);
            return View(await eLECTIONDBnewContext.ToListAsync());
        }

        // GET: CentreElections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centreElection = await _context.CentreElections
                .Include(c => c.Administrateur)
                .FirstOrDefaultAsync(m => m.CentreElectionId == id);
            if (centreElection == null)
            {
                return NotFound();
            }

            return View(centreElection);
        }

        // GET: CentreElections/Create
        public IActionResult Create()
        {
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId");
            return View();
        }

        // POST: CentreElections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CentreElectionId,LibelleCentre,AdresseCentre,AdministrateurId")] CentreElection centreElection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centreElection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId", centreElection.AdministrateurId);
            return View(centreElection);
        }

        // GET: CentreElections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centreElection = await _context.CentreElections.FindAsync(id);
            if (centreElection == null)
            {
                return NotFound();
            }
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId", centreElection.AdministrateurId);
            return View(centreElection);
        }

        // POST: CentreElections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CentreElectionId,LibelleCentre,AdresseCentre,AdministrateurId")] CentreElection centreElection)
        {
            if (id != centreElection.CentreElectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centreElection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentreElectionExists(centreElection.CentreElectionId))
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
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId", centreElection.AdministrateurId);
            return View(centreElection);
        }

        // GET: CentreElections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centreElection = await _context.CentreElections
                .Include(c => c.Administrateur)
                .FirstOrDefaultAsync(m => m.CentreElectionId == id);
            if (centreElection == null)
            {
                return NotFound();
            }

            return View(centreElection);
        }

        // POST: CentreElections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centreElection = await _context.CentreElections.FindAsync(id);
            _context.CentreElections.Remove(centreElection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentreElectionExists(int id)
        {
            return _context.CentreElections.Any(e => e.CentreElectionId == id);
        }
    }
}
