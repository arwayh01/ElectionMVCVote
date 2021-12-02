using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectionVote;
using ElectionVote.Models;

namespace ElectionVote.Controllers
{
    public class VotesController : Controller
    {
        private readonly ELECTIONDBnewContext _context;

        public VotesController(ELECTIONDBnewContext context)
        {
            _context = context;
        }

        // GET: Votes
        public async Task<IActionResult> Index()
        {
            var eLECTIONDBnewContext = _context.Vote.Include(v => v.Candidat).Include(v => v.Electeur);
            return View(await eLECTIONDBnewContext.ToListAsync());
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote
                .Include(v => v.Candidat)
                .Include(v => v.Electeur)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // GET: Votes/Create
        public IActionResult Create()
        {
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId");
            ViewData["ElecteurId"] = new SelectList(_context.Electeurs, "ElecteurId", "ElecteurId");
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoteId,CandidatId,ElecteurId")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId", vote.CandidatId);
            ViewData["ElecteurId"] = new SelectList(_context.Electeurs, "ElecteurId", "ElecteurId", vote.ElecteurId);
            return View(vote);
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId", vote.CandidatId);
            ViewData["ElecteurId"] = new SelectList(_context.Electeurs, "ElecteurId", "ElecteurId", vote.ElecteurId);
            return View(vote);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoteId,CandidatId,ElecteurId")] Vote vote)
        {
            if (id != vote.VoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.VoteId))
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
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "CandidatId", "CandidatId", vote.CandidatId);
            ViewData["ElecteurId"] = new SelectList(_context.Electeurs, "ElecteurId", "ElecteurId", vote.ElecteurId);
            return View(vote);
        }

        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Vote
                .Include(v => v.Candidat)
                .Include(v => v.Electeur)
                .FirstOrDefaultAsync(m => m.VoteId == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vote = await _context.Vote.FindAsync(id);
            _context.Vote.Remove(vote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteExists(int id)
        {
            return _context.Vote.Any(e => e.VoteId == id);
        }
    }
}
