using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class Funcionario
    {
        public int Matricula { get; set; }
        public string Nome {get ;set;}
        public string Cargo { get; set; }
        [Display(Name = "Data de Admissão")]
        [DataType(DataType.Date)]
        public DateTime DataAdmissao { get; set; }

        public virtual IList<AreaPlantio> ListaAreaPlantios { get; set; }
    }
}
