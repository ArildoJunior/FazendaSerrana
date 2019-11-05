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
    public class TipoCulturasController : Controller
    {
        private readonly ModelContext _context;

        public TipoCulturasController(ModelContext context)
        {
            _context = context;
        }



        // GET: TipoCulturas
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.TipoCulturas.Include(t => t.Cultura).Include(t => t.Praga);
            return View(await modelContext.ToListAsync());
        }

        // GET: TipoCulturas/Details/5
        public async Task<IActionResult> Details(int? CodigoCultura, int? CodigoPraga)
        {
            if (CodigoCultura == null || CodigoPraga == null)
            {
                return NotFound();
            }

            var tipoCultura = await _context.TipoCulturas
                .Include(t => t.Cultura)
                .Include(t => t.Praga)
                .FirstOrDefaultAsync(m => m.CodigoCultura == CodigoCultura && m.CodigoPraga == CodigoPraga);
            if (tipoCultura == null)
            {
                return NotFound();
            }

            return View(tipoCultura);
        }

        // GET: TipoCulturas/Create
        public IActionResult Create()
        {
            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome");
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular");
            return View();
        }

        // POST: TipoCulturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoCultura,CodigoPraga")] TipoCultura tipoCultura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCultura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome", tipoCultura.CodigoCultura);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", tipoCultura.CodigoPraga);
            return View(tipoCultura);
        }

        // GET: TipoCulturas/Edit/5
        public async Task<IActionResult> Edit(int? CodigoCultura, int? CodigoPraga)
        {
            if (CodigoCultura == null || CodigoPraga == null)
            {
                return NotFound();
            }

            var tipoCultura = await _context.TipoCulturas
                 .Include(t => t.Cultura)
                 .Include(t => t.Praga)
                 .FirstOrDefaultAsync(m => m.CodigoCultura == CodigoCultura && m.CodigoPraga == CodigoPraga);

            if (tipoCultura == null)
            {
                return NotFound();
            }
            TipoCulturaViewModel model = new TipoCulturaViewModel();
            model.CodigoCultura = tipoCultura.CodigoCultura;
            model.CodigoPraga = tipoCultura.CodigoPraga;
            model.Cultura = tipoCultura.Cultura;
            model.Praga = tipoCultura.Praga;
          //  model.SelecionadoKeys = String.Format("{0},{1}", tipoCultura.CodigoCultura, tipoCultura.CodigoPraga);


            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome", model.Cultura);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", model.CodigoPraga);

            return View(model);
        }

        // POST: TipoCulturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,  ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int CodigoCultura, int CodigoPraga, TipoCulturaViewModel tipoCulturaViewModel, TipoCultura tipoCultura)
        {
            if (CodigoCultura != tipoCultura.CodigoCultura || CodigoPraga != tipoCultura.CodigoPraga)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var culturas = await _context.Culturas
                             .Include(c => c.ListaTipoCulturas)
                             .FirstOrDefaultAsync(c => c.Codigo == CodigoCultura);
                        var pragas = await _context.Pragas
                            .Include(c => c.ListaTipoCulturas)
                            .FirstOrDefaultAsync(c => c.Codigo == CodigoPraga);
                    //removendo range de lista de tipo de cultra
                      _context.TipoCulturas.RemoveRange(culturas.ListaTipoCulturas.Where(m => m.CodigoCultura == CodigoCultura && m.CodigoPraga == CodigoPraga));
                      _context.TipoCulturas.RemoveRange(pragas.ListaTipoCulturas.Where(m => m.CodigoCultura == CodigoCultura && m.CodigoPraga == CodigoPraga));
 

                    var culturasadd = await _context.Culturas
                   .FirstOrDefaultAsync(c => c.Codigo == tipoCulturaViewModel.SelecionadoKeysCultura);
                    var pragasadd = await _context.Pragas
                    .FirstOrDefaultAsync(c => c.Codigo == tipoCulturaViewModel.SelecionadoKeysPraga);

                    tipoCultura.CodigoCultura = culturasadd.Codigo;
                    tipoCultura.Cultura = culturasadd;
                    tipoCultura.CodigoPraga = pragasadd.Codigo;
                    tipoCultura.Praga = pragasadd;


                    _context.Add(tipoCultura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCulturaExists(tipoCultura.CodigoCultura , tipoCultura.CodigoPraga))
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
            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome", tipoCulturaViewModel.SelecionadoKeysCultura);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", tipoCulturaViewModel.SelecionadoKeysPraga);

            return View(tipoCultura);
        }

        // GET: TipoCulturas/Delete/5
        public async Task<IActionResult> Delete(int? CodigoCultura, int? CodigoPraga)
        {
            if (CodigoCultura == null || CodigoPraga == null)
            {
                return NotFound();
            }

            var tipoCultura = await _context.TipoCulturas
                .Include(t => t.Cultura)
                .Include(t => t.Praga)
                .FirstOrDefaultAsync(m => m.CodigoCultura == CodigoCultura && m.CodigoPraga == CodigoPraga);
            if (tipoCultura == null)
            {
                return NotFound();
            }

            return View(tipoCultura);
        }

        // POST: TipoCulturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CodigoCultura ,int CodigoPraga)
        {
            var tipoCultura = await _context.TipoCulturas.FindAsync(CodigoCultura,CodigoPraga);
            _context.TipoCulturas.Remove(tipoCultura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCulturaExists(int CodigoCultura, int codigoPraga)
        {
            return _context.TipoCulturas.Any(e => e.CodigoCultura == CodigoCultura && e.CodigoPraga == codigoPraga);
        }
    }
}
