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
    public class CulturasController : Controller
    {
        private readonly ModelContext _context;

        public CulturasController(ModelContext context)
        {
            _context = context;
        }



        // GET: Culturas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Culturas.ToListAsync());
        }

        // GET: Culturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultura = await _context.Culturas
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cultura == null)
            {
                return NotFound();
            }

            return View(cultura);
        }

        // GET: Culturas/Create
        public IActionResult Create()

        {

            return View();
        }

        // POST: Culturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,TempoMaximo,MesIdeal,MesFinal")] Cultura cultura)
        {
            if (ModelState.IsValid)
            {



                _context.Add(cultura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            return View(cultura);
        }

        // GET: Culturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultura = await _context.Culturas.FindAsync(id);
            if (cultura == null)
            {
                return NotFound();
            }

          CulturaViewModel model = new CulturaViewModel();
            model.Codigo = cultura.Codigo;
            model.MesFinal = cultura.MesFinal;
            model.MesIdeal = cultura.MesIdeal;
            model.Nome = cultura.Nome;
            model.TempoMaximo = cultura.TempoMaximo;

            foreach (var praga in _context.Pragas) 
            {
                bool c = cultura.ListaDePraga.Where(p => p.Codigo == praga.Codigo).Count() > 0;
                PragaSelecionadaViewModel editorViewModel = new PragaSelecionadaViewModel(c, praga);
                model.ListaDePragas.Add(editorViewModel);
            
            
            }


            /*
             * 
             * 
             * 
             *  foreach (var fab in _context.Fabricante)
            {
                bool c = extintor.FabricanteExtintores.Where(f => f.IdFabricante == fab.Cnpj).Count() > 0;
                FabricaSelecionadaViewModel editorViewModel = new FabricaSelecionadaViewModel(c, fab);
                model.FabricanteExtintores.Add(editorViewModel);
            }
            ExtintorDisplayViewModel model = new ExtintorDisplayViewModel();
            model.Codigo = extintor.Codigo;
            model.DataFabricacao = extintor.DataFabricacao;
            model.Tipo = extintor.Tipo;
            model.PontoLocalizacaoSelecionadoKeys = String.Format("{0},{1}", extintor.IdLocalizacao, extintor.NumeroAndar);

            List<PontoLocalizacao> groups = getGroupedPontosLocalizacao();

            ViewData["IdLocalizacao"] = new SelectList(groups, "Id", "DescricaoCompleta");
            foreach (var fab in _context.Fabricante)
            {
                bool c = extintor.FabricanteExtintores.Where(f => f.IdFabricante == fab.Cnpj).Count() > 0;
                FabricaSelecionadaViewModel editorViewModel = new FabricaSelecionadaViewModel(c, fab);
                model.FabricanteExtintores.Add(editorViewModel);
            }
            model.PontosLocalizacao = _context.PontoLocalizacao.ToList();

            ViewBag.IdFabricante = _context.Fabricante.ToList();
            ViewData["IdLocalizacao"] = new SelectList(model.PontosLocalizacao, "ChaveComposta", "Descricao", model.PontoLocalizacaoSelecionadoKeys);

    */


            return View(model);
        }

        // POST: Culturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,TempoMaximo,MesIdeal,MesFinal")] Cultura cultura)
        {
            if (id != cultura.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cultura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CulturaExists(cultura.Codigo))
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
            return View(cultura);
        }

        // GET: Culturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultura = await _context.Culturas
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cultura == null)
            {
                return NotFound();
            }

            return View(cultura);
        }

        // POST: Culturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var cultura = await _context.Culturas.
                 Include(a => a.ListaAreaPlantios)
                .Include(a => a.ListaTipoCulturas)
                 .FirstOrDefaultAsync(a => a.Codigo == id);

                if (cultura.ListaAreaPlantios.Count > 0 || cultura.ListaTipoCulturas.Count > 0)
                {
                    ModelState.AddModelError("Erro", "Não é possivel deletar restrição PK");

                    return RedirectToAction("Delete");
                }
                else
                {
                    _context.Culturas.Remove(cultura);
                    await _context.SaveChangesAsync();
                }
                
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CulturaExists(int id)
        {
            return _context.Culturas.Any(e => e.Codigo == id);
        }
    }
}
