using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class AreaPlantio
    {
        public int Numero { get; set; }
        public string Tamanho { get; set; }

        public string Status { get; set; }
        public List<AreaPlantio> ListaStatus()
        {
            return new List<AreaPlantio>
            {
                new AreaPlantio {  Status = "ATIVO" },
                new AreaPlantio {  Status = "CANCELADO" }
            };
        }
        [Display(Name = "Funcionario")]
        public int IdMatricula { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        [Display(Name = "Data do Plantio")]
        [DataType(DataType.Date)]

        public DateTime DataPlantio { get; set; }
        [Display(Name = "Cultura")]
        public int CodigoCultura { get; set; }

        public virtual Cultura Cultura {get;set;}

        public virtual IList<AplicarAgrotoxico> ListaAplicarAgrotoxicos { get; set; }

    }
}
