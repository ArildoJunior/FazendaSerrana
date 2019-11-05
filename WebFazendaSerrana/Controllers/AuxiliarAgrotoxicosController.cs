using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFazendaSerrana.Models;
using WebFazendaSerrana.Models.Context;
using WebFazendaSerrana.ViewModel;

namespace WebFazendaSerrana.Controllers
{
    public class AuxiliarAgrotoxicosController : Controller
    {
        private readonly ModelContext _context;

        public AuxiliarAgrotoxicosController(ModelContext context)
        {
            _context = context;
        }

        // GET: AuxiliarAgrotoxicos
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.AuxiliarAgrotoxicos.Include(a => a.Agrotoxico).Include(a => a.Praga);
            return View(await modelContext.ToListAsync());
        }

        // GET: AuxiliarAgrotoxicos/Details/5
        public async Task<IActionResult> Details(int? CodigoAgrotoxico, int? CodigoPraga)
        {
            if (CodigoAgrotoxico == null || CodigoPraga == null)
            {
                return NotFound();
            }

            var auxiliarAgrotoxico = await _context.AuxiliarAgrotoxicos
                .Include(a => a.Agrotoxico)
                .Include(a => a.Praga)
                .FirstOrDefaultAsync(m => m.CodigoAgrotoxico == CodigoAgrotoxico && m.CodigoPraga == CodigoPraga);
            if (auxiliarAgrotoxico == null)
            {
                return NotFound();
            }

            return View(auxiliarAgrotoxico);
        }

        // GET: AuxiliarAgrotoxicos/Create
        public IActionResult Create()
        {
            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome");
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular");
            return View();
        }

        // POST: AuxiliarAgrotoxicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoAgrotoxico,CodigoPraga")] AuxiliarAgrotoxico auxiliarAgrotoxico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auxiliarAgrotoxico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome", auxiliarAgrotoxico.CodigoAgrotoxico);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", auxiliarAgrotoxico.CodigoPraga);
            return View(auxiliarAgrotoxico);
        }

        // GET: AuxiliarAgrotoxicos/Edit/5
        public async Task<IActionResult> Edit(int? CodigoAgrotoxico, int? CodigoPraga)
        {
            if (CodigoAgrotoxico == null || CodigoPraga == null)
            {
                return NotFound();
            }
            var auxiliarAgrotoxico = await _context.AuxiliarAgrotoxicos.FindAsync(CodigoAgrotoxico, CodigoPraga);
            if (auxiliarAgrotoxico == null)
            {
                return NotFound();
            }

            AuxiliarAgrotoxicoViewModel model = new AuxiliarAgrotoxicoViewModel();
            model.CodigoAgrotoxico = auxiliarAgrotoxico.CodigoAgrotoxico;
            model.CodigoPraga = auxiliarAgrotoxico.CodigoPraga;
            model.Agrotoxico = auxiliarAgrotoxico.Agrotoxico;
            model.Praga = auxiliarAgrotoxico.Praga;
   
            

            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome", model.CodigoAgrotoxico);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", model.CodigoPraga);
            return View(model);
        }

        // POST: AuxiliarAgrotoxicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost , ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CodigoAgrotoxico,int CodigoPraga, AuxiliarAgrotoxicoViewModel auxiliarAgrotoxicoViewModel, [Bind("CodigoAgrotoxico,CodigoPraga")] AuxiliarAgrotoxico auxiliarAgrotoxico)
        {
            if (CodigoAgrotoxico != auxiliarAgrotoxico.CodigoAgrotoxico || CodigoPraga != auxiliarAgrotoxico.CodigoPraga)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var agrotoxicos = await _context.Agrotoxicos
                        .Include(a => a.ListaAuxiliarAgrotoxicos)
                        .FirstOrDefaultAsync(a => a.Codigo == CodigoAgrotoxico);

                    var pragas = await _context.Pragas
                        .Include(a => a.ListaAuxiliarAgrotoxicos)
                        .FirstOrDefaultAsync(a => a.Codigo == CodigoPraga);

                    _context.AuxiliarAgrotoxicos.RemoveRange(agrotoxicos.ListaAuxiliarAgrotoxicos
                        .Where(a => a.CodigoAgrotoxico == CodigoAgrotoxico && a.CodigoPraga == CodigoPraga));

                    _context.AuxiliarAgrotoxicos.RemoveRange(pragas.ListaAuxiliarAgrotoxicos
                        .Where(a => a.CodigoAgrotoxico == CodigoAgrotoxico && a.CodigoPraga == CodigoPraga));

                    foreach (var item in _context.Agrotoxicos)
                        foreach (var item2 in _context.Pragas)
                        {
                             if (item.Codigo == auxiliarAgrotoxicoViewModel.SelecionadoKeysAgrotoxico)
                            {
                            auxiliarAgrotoxico.CodigoAgrotoxico = item.Codigo;
                            auxiliarAgrotoxico.Agrotoxico = item;
                             }
                            if (item2.Codigo == auxiliarAgrotoxicoViewModel.SelecionadoKeysPraga)
                            {
                                auxiliarAgrotoxico.CodigoPraga = item2.Codigo;
                                auxiliarAgrotoxico.Praga = item2;
                            }
         
                        }
                    _context.Add(auxiliarAgrotoxico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuxiliarAgrotoxicoExists(auxiliarAgrotoxico.CodigoAgrotoxico, auxiliarAgrotoxico.CodigoPraga))
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
            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome", auxiliarAgrotoxico.CodigoAgrotoxico);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", auxiliarAgrotoxico.CodigoPraga);
            return View(auxiliarAgrotoxico);
        }

        // GET: AuxiliarAgrotoxicos/Delete/5
        public async Task<IActionResult> Delete(int? CodigoAgrotoxico, int? CodigoPraga)
        {
            if (CodigoAgrotoxico == null || CodigoPraga == null)
            {
                return NotFound();
            }

            var auxiliarAgrotoxico = await _context.AuxiliarAgrotoxicos
                .Include(a => a.Agrotoxico)
                .Include(a => a.Praga)
                .FirstOrDefaultAsync(m => m.CodigoAgrotoxico == CodigoAgrotoxico && m.CodigoPraga == CodigoPraga);
            if (auxiliarAgrotoxico == null)
            {
                return NotFound();
            }

            return View(auxiliarAgrotoxico);
        }

        // POST: AuxiliarAgrotoxicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CodigoAgrotoxico, int CodigoPraga)
        {
            var auxiliarAgrotoxico = await _context.AuxiliarAgrotoxicos.FindAsync(CodigoAgrotoxico,  CodigoPraga);
            _context.AuxiliarAgrotoxicos.Remove(auxiliarAgrotoxico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuxiliarAgrotoxicoExists(int CodigoAgrotoxico, int CodigoPraga)
        {
            return _context.AuxiliarAgrotoxicos.Any(e => e.CodigoAgrotoxico == CodigoAgrotoxico && e.CodigoPraga == CodigoPraga);
        }
    }
}
