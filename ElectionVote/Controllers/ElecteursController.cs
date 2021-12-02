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
    public class ElecteursController : Controller
    {
        private readonly ELECTIONDBnewContext _context;

        public ElecteursController(ELECTIONDBnewContext context)
        {
            _context = context;
        }

        // GET: Electeurs
        public async Task<IActionResult> Index()
        {
            var eLECTIONDBnewContext = _context.Electeurs.Include(e => e.CentreElection).Include(e => e.Condidatcandidat);
            return View(await eLECTIONDBnewContext.ToListAsync());
        }

        // GET: Electeurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electeur = await _context.Electeurs
                .Include(e => e.CentreElection)
                .Include(e => e.Condidatcandidat)
                .FirstOrDefaultAsync(m => m.ElecteurId == id);
            if (electeur == null)
            {
                return NotFound();
            }

            return View(electeur);
        }

        // GET: Electeurs/Create
        public IActionResult Create()
        {
            ViewData["CentreElectionId"] = new SelectList(_context.CentreElections, "CentreElectionId", "CentreElectionId");
            ViewData["CondidatcandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId");
            return View();
        }

        // POST: Electeurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElecteurId,NomElecteur,PrenomElecteur,CinElecteur,GenreElecteur,CentreElectionId,CondidatcandidatId")] Electeur electeur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(electeur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CentreElectionId"] = new SelectList(_context.CentreElections, "CentreElectionId", "CentreElectionId", electeur.CentreElectionId);
            ViewData["CondidatcandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId", electeur.CondidatcandidatId);
            return View(electeur);
        }

        // GET: Electeurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electeur = await _context.Electeurs.FindAsync(id);
            if (electeur == null)
            {
                return NotFound();
            }
            ViewData["CentreElectionId"] = new SelectList(_context.CentreElections, "CentreElectionId", "CentreElectionId", electeur.CentreElectionId);
            ViewData["CondidatcandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId", electeur.CondidatcandidatId);
            return View(electeur);
        }

        // POST: Electeurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElecteurId,NomElecteur,PrenomElecteur,CinElecteur,GenreElecteur,CentreElectionId,CondidatcandidatId")] Electeur electeur)
        {
            if (id != electeur.ElecteurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electeur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElecteurExists(electeur.ElecteurId))
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
            ViewData["CentreElectionId"] = new SelectList(_context.CentreElections, "CentreElectionId", "CentreElectionId", electeur.CentreElectionId);
            ViewData["CondidatcandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId", electeur.CondidatcandidatId);
            return View(electeur);
        }

        // GET: Electeurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electeur = await _context.Electeurs
                .Include(e => e.CentreElection)
                .Include(e => e.Condidatcandidat)
                .FirstOrDefaultAsync(m => m.ElecteurId == id);
            if (electeur == null)
            {
                return NotFound();
            }

            return View(electeur);
        }

        // POST: Electeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var electeur = await _context.Electeurs.FindAsync(id);
            _context.Electeurs.Remove(electeur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ElecteurExists(int id)
        {
            return _context.Electeurs.Any(e => e.ElecteurId == id);
        }
    }
}
