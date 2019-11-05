using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFazendaSerrana.Models;

namespace WebFazendaSerrana.ViewModel
{
    public class TipoCulturaViewModel
    {
        [Display(Name = "Cultura")]
        public int CodigoCultura { get; set; }
        public virtual Cultura Cultura { get; set; }
        [Display(Name = "Praga")]
        public int CodigoPraga { get; set; }

        public virtual Praga Praga { get; set; }

        public int SelecionadoKeysCultura { get; set; }
        public int SelecionadoKeysPraga{ get; set; }


    }


}

