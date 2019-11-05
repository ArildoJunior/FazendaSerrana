using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFazendaSerrana.Models;

namespace WebFazendaSerrana.ViewModel
{
    public class AuxiliarAgrotoxicoViewModel
    {
        [Display(Name = "Agrotoxico")]
        public int CodigoAgrotoxico { get; set; }
        public virtual Agrotoxico Agrotoxico { get; set; }
        [Display(Name = "Praga")]
        public int CodigoPraga { get; set; }

        public virtual Praga Praga { get; set; }

        public int SelecionadoKeysAgrotoxico { get; set; }
        public int SelecionadoKeysPraga { get; set; }


    }
}
