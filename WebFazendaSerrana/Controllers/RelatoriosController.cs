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
    public class RelatoriosController : Controller
    {
        private readonly ModelContext _context;


        public RelatoriosController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ObterCulturaPraga(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var pragas = await _context.Pragas.FirstAsync(a => a.Codigo == id);

            var resultado = (from praga in _context.Pragas
                             join tp in _context.TipoCulturas on pragas equals tp.Praga
                             join cultura in _context.Culturas on tp.Cultura equals cultura
                             where (praga.Codigo == id)
                             select new
                             {
                                 nomecultura = cultura.Nome,
                                 epoca = praga.EstacaoAno,
                                 ultimavez = praga.DataUltimaPraga

                             }).ToList().GroupBy(epoca => epoca.epoca);



            return View(resultado);

        }
        public async Task<IActionResult> ObterAgrotoxicoPraga(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var pragas = await _context.Pragas.FirstAsync(a => a.Codigo == id);

            var resultado = (from praga in _context.Pragas
                             join aux in _context.AuxiliarAgrotoxicos on pragas.Codigo equals aux.Praga.Codigo
                             join agrotoxico in _context.Agrotoxicos on aux.Agrotoxico.Codigo equals agrotoxico.Codigo
                             where (praga.Codigo == id)
                             select new
                             {
                                 nomeCientifico = praga.NomeCientifico,
                                 nomePopular = praga.NomePopular,
                                 agrotoxicos = agrotoxico.Nome


                             }).ToList();
            //DUVIDA PROFESSOR PQ NÃO TRAS IGUAIS
            //QUERY C#
            /*
                var resultado = (from praga in _context.Pragas
                             join aux in _context.AuxiliarAgrotoxicos on pragas.Codigo equals aux.Praga.Codigo
                             join agrotoxico in _context.Agrotoxicos on aux.Agrotoxico.Codigo equals agrotoxico.Codigo
                             where (aux.CodigoPraga == id) 
                             select new
                             {
                                nomeCientifico = praga.NomeCientifico,
                                nomePopular = praga.NomePopular,
                                agrotoxicos = agrotoxico.Nome
                             }).ToList();
             */
            //QUERY SQL 
            /*select TB_PRAGA.NomeCientifico,TB_PRAGA.NomePopular,TB_AGROTOXICO.Nome from TB_PRAGA,TB_AUXILIAR_AGROTOXICO,TB_AGROTOXICO
            where TB_PRAGA.Codigo = TB_AUXILIAR_AGROTOXICO.CodigoPraga
            and TB_AUXILIAR_AGROTOXICO.CodigoAgrotoxico = TB_AGROTOXICO.Codigo
            AND TB_AUXILIAR_AGROTOXICO.CodigoPraga = 1
            */

            return View(resultado);

        }

        public async Task<IActionResult> ObterPraga(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var pragas = await _context.TipoCulturas.FirstAsync(a => a.CodigoPraga == id);

            var resultado = _context.TipoCulturas
                .Include(p => p.Praga)
                .Where(p => p.CodigoPraga == id)
                .Select(cultura => new
                {
                    nomecultura = cultura.Cultura.Nome,
                    epocaEstacao = cultura.Praga.EstacaoAno,
                    ultimaVez = cultura.Praga.DataUltimaPraga

                }).ToList()
                .GroupBy(p => p.epocaEstacao);

            return View(resultado);

        }
        public async Task<ActionResult> ObterQtdAgrotoxicoPorArea(String Codarea)
        {

            if (Codarea == null)
            {

                return NotFound();
            }

            var resultado = await (from area in _context.AplicarAgrotoxicos
                                   join agro in _context.Agrotoxicos
                                   on area.Agrotoxico equals agro
                                   where (area.AreaPlantio.Tamanho.Equals(Codarea) && area.Tipo.Equals("PREVENTIVA"))
                                   select new
                                   {
                                       area = area.AreaPlantio.Tamanho,
                                       qtdAplicado = area.QtdAplicado,
                                       nomeAgro = agro.Nome,
                                       qtdDisponivel = agro.QtdDisponivel,
                                       unidadeMedida = agro.UnidadeMedida

                                   }).ToListAsync();


            return View(resultado);


        }

        public async Task<ActionResult> ObterAgrotoxicoAplicadoPorPraga(String NomeCientifico)
        {

            if (NomeCientifico == null)
            {

                return NotFound();
            }

            var idpraga = await _context.Pragas.FirstAsync(x => x.NomeCientifico.Equals(NomeCientifico));

            var resultado = (from area in _context.AplicarAgrotoxicos
                             join agro in _context.Agrotoxicos
                             on area.Agrotoxico equals agro
                             join pragas in _context.Pragas
                             on area.Praga equals pragas
                             where (area.Praga.NomeCientifico.Equals(NomeCientifico) && area.Tipo.Equals("CORRETIVA") && area.AreaPlantio.Status.Equals("ATIVO"))
                             select new
                             {
                                 area = area.AreaPlantio.Tamanho,
                                 nomeAgro = agro.Nome,
                                 qtdAplicado = area.QtdAplicado,
                             }).ToList();

            return View(idpraga);

        }

        public async Task<ActionResult> ObterMesIdealCultura()
        {
            var culturas = await (from cultura in _context.Culturas
                                  select new
                                  {
                                      nome = cultura.Nome,
                                      mesIdeal = cultura.MesIdeal,
                                      mesFinal = cultura.MesFinal

                                  }).ToListAsync();
            return View(culturas);
        }
        public async Task<ActionResult> ObterAreaPlantioModificarCultura()
        {

            var resultado = await (from areas in _context.AreaPlantios
                                   join cultura in _context.Culturas
                                   on areas.Cultura equals cultura
                                   where (areas.DataPlantio > cultura.TempoMaximo)
                                   select new
                                   {
                                       area = areas.Tamanho,
                                       cultura = cultura.Nome

                                   }).ToListAsync();
            return View(resultado);



        }
        public async Task<IActionResult> ObterAreaPlantio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaPlantio = await _context.AreaPlantios.FindAsync(id);



            var resultado = _context.AreaPlantios
                .Include(cultura => cultura.Cultura)
                .Where(area => area.Numero == id && area.Status.Equals("ATIVO"))
                .Select(area => new
                {

                    Area = area.Tamanho,
                    NomeCultura = area.Cultura.Nome,
                    Iniciada = area.DataPlantio,
                    status = area.Status

                }).FirstOrDefault();



            if (resultado == null)
            {
                return NotFound();
            }

            return View(resultado);

        }
        public async Task<IActionResult> ObterFuncionario(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);

            #region Consultas Funcionario
            var result1 = _context.Funcionarios.Include(func => func.ListaAreaPlantios)
                                               .Where(func => func.Matricula.Equals(id))
                                               .Select(func => new
                                               {
                                                   nome = func.Nome,
                                                   cargo = func.Cargo,
                                                   admissao = func.DataAdmissao,
                                                   areasPlantio =
                                                            func.ListaAreaPlantios.Select(area =>
                                                            area.Numero)
                                               }).FirstOrDefault();

            #endregion

            if (funcionario == null)
            {
                return NotFound();
            }


            return View(funcionario);


        }



        // GET: Relatorios
        public ActionResult Index()
        {

            return View();
        }

    }
}