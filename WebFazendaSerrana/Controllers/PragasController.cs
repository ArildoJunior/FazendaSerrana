using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFazendaSerrana.Models;
using WebFazendaSerrana.Models.Context;

namespace WebFazendaSerrana.Controllers
{
    public class PragasController : Controller
    {
        private readonly ModelContext _context;

        public PragasController(ModelContext context)
        {
            _context = context;
        }

        // GET: Pragas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pragas.ToListAsync());
        }



        // GET: Pragas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var praga = await _context.Pragas
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (praga == null)
            {
                return NotFound();
            }

            return View(praga);
        }

        // GET: Pragas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pragas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,NomeCientifico,NomePopular,EstacaoAno,DataUltimaPraga")] Praga praga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(praga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(praga);
        }

        // GET: Pragas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var praga = await _context.Pragas.FindAsync(id);
            if (praga == null)
            {
                return NotFound();
            }
            return View(praga);
        }

        // POST: Pragas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,NomeCientifico,NomePopular,EstacaoAno,DataUltimaPraga")] Praga praga)
        {
            if (id != praga.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(praga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PragaExists(praga.Codigo))
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
            return View(praga);
        }

        // GET: Pragas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var praga = await _context.Pragas
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (praga == null)
            {
                return NotFound();
            }

            return View(praga);
        }

        // POST: Pragas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var praga = await _context.Pragas.
                 Include(a => a.ListaAplicarAgrotoxicos)
                .Include(a => a.ListaAuxiliarAgrotoxicos)
                 .FirstOrDefaultAsync(a => a.Codigo == id);

                if (praga.ListaAplicarAgrotoxicos.Count > 0 || praga.ListaAuxiliarAgrotoxicos.Count > 0)
                {
                    ModelState.AddModelError("Erro", "Não é possivel deletar restrição PK");

                    return RedirectToAction("Delete");
                }
                else
                {
                    _context.Pragas.Remove(praga);
                    await _context.SaveChangesAsync();
                }

            }
            return RedirectToAction(nameof(Index));
        }

        private bool PragaExists(int id)
        {
            return _context.Pragas.Any(e => e.Codigo == id);
        }
    }
}
