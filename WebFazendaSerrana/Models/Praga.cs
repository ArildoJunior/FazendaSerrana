using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class Praga
    {
        [Display(Name = "CodigoPraga")]
        public int Codigo { get; set; }
        public string NomeCientifico { get; set; }
        public string NomePopular { get; set; }

        public string EstacaoAno { get; set; }
        [Display(Name = "Data da Ultima Praga")]
        [DataType(DataType.Date)]
        public DateTime DataUltimaPraga { get; set; }
        public virtual IList<TipoCultura> ListaTipoCulturas { get; set; }
        public virtual IList<AuxiliarAgrotoxico> ListaAuxiliarAgrotoxicos { get; set; }
        public virtual IList<AplicarAgrotoxico> ListaAplicarAgrotoxicos { get; set; }
    }
    }
