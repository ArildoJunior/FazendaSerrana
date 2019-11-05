using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class AplicarAgrotoxico
    {
        public int IdAplicarAgrotoxico { get; set; }
        [Display(Name = "AreaPlantio")]
        public int NumeroAreaPlantio { get; set; }
        public virtual AreaPlantio AreaPlantio { get; set; }
        [Display(Name = "Agrotoxico")]
        public int CodigoAgrotoxico { get; set; }
        public Agrotoxico Agrotoxico { get; set; }
        public int QtdAplicado { get; set; }
        public string Tipo { get; set; }
        public List<AplicarAgrotoxico> ListaTipos()
        {
            return new List<AplicarAgrotoxico>
            {
                new AplicarAgrotoxico {  Tipo = "CORRETIVA" },
                new AplicarAgrotoxico {  Tipo = "PREVENTIVA" }
            };
        }

        [Display(Name = "Praga")]
        public int CodigoPraga { get; set; }
        public virtual Praga Praga { get; set; }
    }
}
