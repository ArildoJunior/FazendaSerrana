using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class Cultura
    {
        [Display(Name = "CodigoCultura")]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Tempo Maximo da Plantação")]
        [DataType(DataType.Date)]
        public DateTime TempoMaximo { get; set; }
        public string MesIdeal { get; set; }
        public string MesFinal { get; set; }

        public virtual IList<AreaPlantio> ListaAreaPlantios { get; set; }
        public virtual IList<TipoCultura> ListaTipoCulturas { get; set; }
        public virtual List<Praga> ListaDePraga { get; set; }

        public Cultura()
        {
            this.ListaAreaPlantios = new List<AreaPlantio>();
            this.ListaTipoCulturas = new List<TipoCultura>();
            this.ListaDePraga = new List<Praga>();

        }
    }

}
