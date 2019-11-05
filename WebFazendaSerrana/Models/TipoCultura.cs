using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class TipoCultura
    {
       // [Display(Name = "Cultura")]
        public int CodigoCultura { get; set; }
        public virtual Cultura Cultura { get; set; }
      //[Display(Name = "Praga")]
        public int CodigoPraga { get; set; }

        public virtual Praga Praga { get; set; }


       /* public String DescricaoCompletaCultura
        {
            get { return String.Format("Cultura: {0}", this.Cultura.Nome); }
        }
        public String DescricaoCompletaPraga
        {
            get { return String.Format("Praga: {0}", this.Praga.NomePopular); }
        }

        public String ChaveComposta
        {
            get { return String.Format("{0},{1}", this.CodigoCultura, this.CodigoPraga); }
        }
        */
    }


}

