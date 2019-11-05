using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class AuxiliarAgrotoxicoMap
    {
        

        public AuxiliarAgrotoxicoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuxiliarAgrotoxico>(aux =>
            {
                aux.ToTable("TB_AUXILIAR_AGROTOXICO");
                aux.HasKey(a => new { a.CodigoAgrotoxico, a.CodigoPraga });


                aux.HasOne(a => a.Agrotoxico)
                .WithMany(ag => ag.ListaAuxiliarAgrotoxicos)
                .HasForeignKey(a => a.CodigoAgrotoxico);


                aux.HasOne(a => a.Praga)
                .WithMany(p => p.ListaAuxiliarAgrotoxicos)
                .HasForeignKey(a => a.CodigoPraga);
            });
        }
    }
}
