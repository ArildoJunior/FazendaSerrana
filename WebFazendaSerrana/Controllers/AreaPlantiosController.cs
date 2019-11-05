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
    public class AreaPlantiosController : Controller
    {
        private readonly ModelContext _context;

        public AreaPlantiosController(ModelContext context)
        {
            _context = context;
        }


     
        // GET: AreaPlantios
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.AreaPlantios.Include(a => a.Cultura).Include(a => a.Funcionario);
            return View(await modelContext.ToListAsync());
        }

        // GET: AreaPlantios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaPlantio = await _context.AreaPlantios
                .Include(a => a.Cultura)
                .Include(a => a.Funcionario)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (areaPlantio == null)
            {
                return NotFound();
            }

            return View(areaPlantio);
        }

        // GET: AreaPlantios/Create
        public IActionResult Create()
        {
            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome");
            ViewData["IdMatricula"] = new SelectList(_context.Funcionarios, "Matricula", "Nome");
            ViewBag.ListaStatus = new SelectList(new AreaPlantio().ListaStatus(), "Status", "Status");
            return View();
        }

        // POST: AreaPlantios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,Tamanho,Status,IdMatricula,DataPlantio,CodigoCultura")] AreaPlantio areaPlantio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaPlantio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome", areaPlantio.CodigoCultura);
            ViewData["IdMatricula"] = new SelectList(_context.Funcionarios, "Matricula", "Nome", areaPlantio.IdMatricula);
            ViewBag.ListaStatus = new SelectList(new AreaPlantio().ListaStatus(), "Status", "Status");
            return View(areaPlantio);
        }

        // GET: AreaPlantios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaPlantio = await _context.AreaPlantios.FindAsync(id);
            if (areaPlantio == null)
            {
                return NotFound();
            }
            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome", areaPlantio.CodigoCultura);
            ViewData["IdMatricula"] = new SelectList(_context.Funcionarios, "Matricula", "Nome", areaPlantio.IdMatricula);
            ViewBag.ListaStatus = new SelectList(new AreaPlantio().ListaStatus(), "Status", "Status");
            return View(areaPlantio);
        }

        // POST: AreaPlantios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Numero,Tamanho,Status,IdMatricula,DataPlantio,CodigoCultura")] AreaPlantio areaPlantio)
        {
            if (id != areaPlantio.Numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areaPlantio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaPlantioExists(areaPlantio.Numero))
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
            ViewData["CodigoCultura"] = new SelectList(_context.Culturas, "Codigo", "Nome", areaPlantio.CodigoCultura);
            ViewData["IdMatricula"] = new SelectList(_context.Funcionarios, "Matricula", "Nome", areaPlantio.IdMatricula);
            ViewBag.ListaStatus = new SelectList(new AreaPlantio().ListaStatus(), "Status", "Status");
            return View(areaPlantio);
        }

        // GET: AreaPlantios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaPlantio = await _context.AreaPlantios
                .Include(a => a.Cultura)
                .Include(a => a.Funcionario)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (areaPlantio == null)
            {
                return NotFound();
            }

            return View(areaPlantio);
        }

        // POST: AreaPlantios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var areaPlantio = await _context.AreaPlantios.FindAsync(id);
            _context.AreaPlantios.Remove(areaPlantio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaPlantioExists(int id)
        {
            return _context.AreaPlantios.Any(e => e.Numero == id);
        }
    }
}
