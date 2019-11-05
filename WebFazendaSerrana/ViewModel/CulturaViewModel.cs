using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFazendaSerrana.Models;

namespace WebFazendaSerrana.ViewModel
{
    public class CulturaViewModel
    {
        [Display(Name = "CodigoCultura")]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Tempo Maximo da Plantação")]
        [DataType(DataType.Date)]
        public DateTime TempoMaximo { get; set; }
        public string MesIdeal { get; set; }
        public string MesFinal { get; set; }
        public Praga praga;
        

        public virtual  List<AreaPlantio> ListaAreaPlantios { get; set; }
        public virtual  List<TipoCultura> ListaTipoCulturas { get; set; }

        public virtual  List<PragaSelecionadaViewModel> ListaDePragas { get; set; }


        public CulturaViewModel()
        {
            this.ListaAreaPlantios = new List<AreaPlantio>();
            this.ListaTipoCulturas = new List<TipoCultura>();
            this.ListaDePragas = new List<PragaSelecionadaViewModel>();
        }
        public CulturaViewModel(Cultura cultura) : this()
        {
            this.Codigo = cultura.Codigo;
            this.MesIdeal = cultura.MesIdeal;
            this.MesFinal = cultura.MesFinal;
            this.Nome = cultura.Nome;
            this.TempoMaximo = cultura.TempoMaximo;
            


            if (cultura.ListaDePraga != null)
            {
                foreach (var item in cultura.ListaDePraga)
                {
                    this.ListaDePragas.Add(new PragaSelecionadaViewModel(true, item));

                }
            }

        }

    }
}
