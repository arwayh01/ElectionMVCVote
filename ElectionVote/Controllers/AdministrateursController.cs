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
    public class AdministrateursController : Controller
    {
        private readonly ELECTIONDBnewContext _context;

        public AdministrateursController(ELECTIONDBnewContext context)
        {
            _context = context;
        }

        // GET: Administrateurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administrateurs.ToListAsync());
        }

        // GET: Administrateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrateur = await _context.Administrateurs
                .FirstOrDefaultAsync(m => m.AdministrateurId == id);
            if (administrateur == null)
            {
                return NotFound();
            }

            return View(administrateur);
        }

        // GET: Administrateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdministrateurId,NomAdmin,PrenomAdmin,CinAdmin,MotDePasse")] Administrateur administrateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administrateur);
        }

        // GET: Administrateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrateur = await _context.Administrateurs.FindAsync(id);
            if (administrateur == null)
            {
                return NotFound();
            }
            return View(administrateur);
        }

        // POST: Administrateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdministrateurId,NomAdmin,PrenomAdmin,CinAdmin,MotDePasse")] Administrateur administrateur)
        {
            if (id != administrateur.AdministrateurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrateurExists(administrateur.AdministrateurId))
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
            return View(administrateur);
        }

        // GET: Administrateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrateur = await _context.Administrateurs
                .FirstOrDefaultAsync(m => m.AdministrateurId == id);
            if (administrateur == null)
            {
                return NotFound();
            }

            return View(administrateur);
        }

        // POST: Administrateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrateur = await _context.Administrateurs.FindAsync(id);
            _context.Administrateurs.Remove(administrateur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrateurExists(int id)
        {
            return _context.Administrateurs.Any(e => e.AdministrateurId == id);
        }
    }
}
