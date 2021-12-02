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
    public class CandidatsController : Controller
    {
        private readonly ELECTIONDBnewContext _context;

        public CandidatsController(ELECTIONDBnewContext context)
        {
            _context = context;
        }

        // GET: Candidats
        public async Task<IActionResult> Index()
        {
            var eLECTIONDBnewContext = _context.Candidats.Include(c => c.Administrateur);
            return View(await eLECTIONDBnewContext.ToListAsync());
        }

        // GET: Candidats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats
                .Include(c => c.Administrateur)
                .FirstOrDefaultAsync(m => m.CandidatId == id);
            if (candidat == null)
            {
                return NotFound();
            }

            return View(candidat);
        }

        // GET: Candidats/Create
        public IActionResult Create()
        {
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId");
            return View();
        }

        // POST: Candidats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidatId,NomCandidat,PrenomCandidat,CinCandidat,ImageCandidat,AdministrateurId")] Candidat candidat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId", candidat.AdministrateurId);
            return View(candidat);
        }

        // GET: Candidats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId", candidat.AdministrateurId);
            return View(candidat);
        }

        // POST: Candidats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CandidatId,NomCandidat,PrenomCandidat,CinCandidat,ImageCandidat,AdministrateurId")] Candidat candidat)
        {
            if (id != candidat.CandidatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatExists(candidat.CandidatId))
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
            ViewData["AdministrateurId"] = new SelectList(_context.Administrateurs, "AdministrateurId", "AdministrateurId", candidat.AdministrateurId);
            return View(candidat);
        }

        // GET: Candidats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats
                .Include(c => c.Administrateur)
                .FirstOrDefaultAsync(m => m.CandidatId == id);
            if (candidat == null)
            {
                return NotFound();
            }

            return View(candidat);
        }

        // POST: Candidats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidat = await _context.Candidats.FindAsync(id);
            _context.Candidats.Remove(candidat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidatExists(int id)
        {
            return _context.Candidats.Any(e => e.CandidatId == id);
        }
    }
}
