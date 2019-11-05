using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFazendaSerrana.Models;

namespace WebFazendaSerrana.ViewModel
{
    public class PragaSelecionadaViewModel
    {
        public bool Checked { get; set; }
        public Praga Praga { get; set; }

        public PragaSelecionadaViewModel() { }
        public PragaSelecionadaViewModel(bool check, Praga praga)
        {
            this.Checked = check;
            this.Praga = praga;
        }
    }

}
