using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFazendaSerrana.Models
{
    public class AuxiliarAgrotoxico
    {
        public int CodigoAgrotoxico { get; set; }
        public virtual Agrotoxico Agrotoxico { get; set; }
        public int CodigoPraga { get; set; }

        public virtual Praga Praga { get; set; }
    }
}
