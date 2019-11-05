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
    public class AgrotoxicosController : Controller
    {
        private readonly ModelContext _context;

        public AgrotoxicosController(ModelContext context)
        {
            _context = context;
        }

        // GET: Agrotoxicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agrotoxicos.ToListAsync());
        }

        // GET: Agrotoxicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrotoxico = await _context.Agrotoxicos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (agrotoxico == null)
            {
                return NotFound();
            }

            return View(agrotoxico);
        }

        // GET: Agrotoxicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agrotoxicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,QtdDisponivel,UnidadeMedida")] Agrotoxico agrotoxico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agrotoxico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agrotoxico);
        }

        // GET: Agrotoxicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrotoxico = await _context.Agrotoxicos.FindAsync(id);
            if (agrotoxico == null)
            {
                return NotFound();
            }
            return View(agrotoxico);
        }

        // POST: Agrotoxicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,QtdDisponivel,UnidadeMedida")] Agrotoxico agrotoxico)
        {
            if (id != agrotoxico.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agrotoxico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgrotoxicoExists(agrotoxico.Codigo))
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
            return View(agrotoxico);
        }

        // GET: Agrotoxicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrotoxico = await _context.Agrotoxicos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (agrotoxico == null)
            {
                return NotFound();
            }

            return View(agrotoxico);
        }

        // POST: Agrotoxicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var agrotoxico = await _context.Agrotoxicos.
                 Include(a => a.ListaAplicarAgrotoxicos)
                .Include(a => a.ListaAuxiliarAgrotoxicos)
                 .FirstOrDefaultAsync(a => a.Codigo == id);

                if (agrotoxico.ListaAuxiliarAgrotoxicos.Count > 0 || agrotoxico.ListaAplicarAgrotoxicos.Count > 0)
                {
                    ModelState.AddModelError("Erro", "Não é possivel deletar restrição PK");
                   
                    return RedirectToAction("Delete") ;
                }
                else
                {
                    _context.Agrotoxicos.Remove(agrotoxico);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AgrotoxicoExists(int id)
        {
            return _context.Agrotoxicos.Any(e => e.Codigo == id);
        }
    }
}
