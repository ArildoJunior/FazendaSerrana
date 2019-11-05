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
    public class AplicarAgrotoxicosController : Controller
    {
        private readonly ModelContext _context;

        public AplicarAgrotoxicosController(ModelContext context)
        {
            _context = context;
        }



      
        // GET: AplicarAgrotoxicos
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.AplicarAgrotoxicos
                .Include(a => a.Agrotoxico)
                .Include(a => a.AreaPlantio)
                .Include(a => a.Praga);




            return View(await modelContext.ToListAsync());
        }

        // GET: AplicarAgrotoxicos/Details/5
        public async Task<IActionResult> Details(int? IdAplicarAgrotoxico)
        {
            if (IdAplicarAgrotoxico == null)
            {
                return NotFound();
            }

            var aplicarAgrotoxico = await _context.AplicarAgrotoxicos
              .Include(ap => ap.AreaPlantio)
              .Include(ap => ap.Agrotoxico)
              .Include(ap => ap.Praga)
              .FirstOrDefaultAsync(ap => ap.IdAplicarAgrotoxico == IdAplicarAgrotoxico);

            if (aplicarAgrotoxico == null)
            {
                return NotFound();
            }

            return View(aplicarAgrotoxico);
        }

        // GET: AplicarAgrotoxicos/Create
        public IActionResult Create()
        {
            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome");
            ViewData["NumeroAreaPlantio"] = new SelectList(_context.AreaPlantios, "Numero", "Tamanho");
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular");
            ViewBag.ListaTipos = new SelectList ( new AplicarAgrotoxico().ListaTipos(), "Tipo", "Tipo" );

            return View();
        }

        // POST: AplicarAgrotoxicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroAreaPlantio,CodigoAgrotoxico,QtdAplicado,Tipo,CodigoPraga")] AplicarAgrotoxico aplicarAgrotoxico)
        {
            if (ModelState.IsValid)
            {
                var agrotoxico = await _context.Agrotoxicos
                  .FirstAsync(m => m.Codigo == aplicarAgrotoxico.CodigoAgrotoxico);
                if(aplicarAgrotoxico.QtdAplicado > 0 && aplicarAgrotoxico.QtdAplicado  <= agrotoxico.QtdDisponivel)
                {
                    agrotoxico.QtdDisponivel = agrotoxico.QtdDisponivel - aplicarAgrotoxico.QtdAplicado;
                    _context.Add(aplicarAgrotoxico);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Erro", "qtd de agrotoxico não disponivel");

                    return NotFound("Erro, Qtd de agrotoxico não disponivel");
                }

               
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome", aplicarAgrotoxico.CodigoAgrotoxico);
            ViewData["NumeroAreaPlantio"] = new SelectList(_context.AreaPlantios, "Numero", "Tamanho", aplicarAgrotoxico.NumeroAreaPlantio);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", aplicarAgrotoxico.CodigoPraga);
            ViewBag.ListaTipos = new SelectList(new AplicarAgrotoxico().ListaTipos(), "Tipo", "Tipo");
            return View(aplicarAgrotoxico);
        }

        // GET: AplicarAgrotoxicos/Edit/5
        public async Task<IActionResult> Edit(int? IdAplicarAgrotoxico)
        {
            if (IdAplicarAgrotoxico == null)
            {
                return NotFound();
            }

            var aplicarAgrotoxico = await _context.AplicarAgrotoxicos.FindAsync(IdAplicarAgrotoxico);

            if (aplicarAgrotoxico == null)
            {
                return NotFound();
            }
            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome", aplicarAgrotoxico.CodigoAgrotoxico);
            ViewData["NumeroAreaPlantio"] = new SelectList(_context.AreaPlantios, "Numero", "Tamanho", aplicarAgrotoxico.NumeroAreaPlantio);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", aplicarAgrotoxico.CodigoPraga);
            ViewBag.ListaTipos = new SelectList(new AplicarAgrotoxico().ListaTipos(), "Tipo", "Tipo");
            return View(aplicarAgrotoxico);
        }

        // POST: AplicarAgrotoxicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdAplicarAgrotoxico, [Bind("IdAplicarAgrotoxico,NumeroAreaPlantio,CodigoAgrotoxico,QtdAplicado,Tipo,CodigoPraga")] AplicarAgrotoxico aplicarAgrotoxico)
        {
            if (IdAplicarAgrotoxico != aplicarAgrotoxico.IdAplicarAgrotoxico)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var agrotoxico = await _context.Agrotoxicos
                  .FirstAsync(m => m.Codigo == aplicarAgrotoxico.CodigoAgrotoxico);

                if (aplicarAgrotoxico.QtdAplicado > 0 && aplicarAgrotoxico.QtdAplicado <= agrotoxico.QtdDisponivel)
                {
                    agrotoxico.QtdDisponivel = agrotoxico.QtdDisponivel - aplicarAgrotoxico.QtdAplicado;
                    
                    _context.Update(aplicarAgrotoxico);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("Erro", "qtd de agrotoxico não disponivel");

                    return NotFound("Erro, Qtd de agrotoxico não disponivel");
                }



                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAgrotoxico"] = new SelectList(_context.Agrotoxicos, "Codigo", "Nome", aplicarAgrotoxico.CodigoAgrotoxico);
            ViewData["NumeroAreaPlantio"] = new SelectList(_context.AreaPlantios, "Numero", "Tamanho", aplicarAgrotoxico.NumeroAreaPlantio);
            ViewData["CodigoPraga"] = new SelectList(_context.Pragas, "Codigo", "NomePopular", aplicarAgrotoxico.CodigoPraga);
            ViewBag.ListaTipos = new SelectList(new AplicarAgrotoxico().ListaTipos(), "Tipo", "Tipo");
            return View(aplicarAgrotoxico);
        }

        // GET: AplicarAgrotoxicos/Delete/5
        public async Task<IActionResult> Delete(int? IdAplicarAgrotoxico)
        {
            if (IdAplicarAgrotoxico == null)
            {
                return NotFound();
            }

            var aplicarAgrotoxico = await _context.AplicarAgrotoxicos
                .Include(a => a.AreaPlantio)
                .Include(a => a.Agrotoxico)
                .Include(ap => ap.Praga)
                .FirstOrDefaultAsync( a => a.IdAplicarAgrotoxico == IdAplicarAgrotoxico);
              
            if (aplicarAgrotoxico == null)
            {
                return NotFound();
            }

            return View(aplicarAgrotoxico);
        }

        // POST: AplicarAgrotoxicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdAplicarAgrotoxico)
        {
            var aplicarAgrotoxico = await _context.AplicarAgrotoxicos.FindAsync(IdAplicarAgrotoxico);
            _context.AplicarAgrotoxicos.Remove(aplicarAgrotoxico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AplicarAgrotoxicoExists(int IdAplicarAgrotoxico)
        {
            return _context.AplicarAgrotoxicos.Any(e => e.IdAplicarAgrotoxico == IdAplicarAgrotoxico);
        }
    }
}
