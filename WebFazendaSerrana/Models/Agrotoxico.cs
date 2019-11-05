using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class Agrotoxico
    {
        [Display(Name = "CodigoAgrotoxico")]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int QtdDisponivel { get; set; }

        public int UnidadeMedida { get; set; }

        public virtual IList<AuxiliarAgrotoxico> ListaAuxiliarAgrotoxicos { get; set; }
        public virtual IList<AplicarAgrotoxico> ListaAplicarAgrotoxicos { get; set; }

    }
}
