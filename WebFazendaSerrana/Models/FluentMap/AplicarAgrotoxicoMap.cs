using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class AplicarAgrotoxicoMap
    {
       

        public AplicarAgrotoxicoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AplicarAgrotoxico>(ag =>
            {
                ag.ToTable("TB_APLICAR_AGROTOXICO");
                ag.HasKey(a => a.IdAplicarAgrotoxico);


                //FK AREA PLANTIO 
                ag.HasOne(a => a.AreaPlantio)
                .WithMany(p => p.ListaAplicarAgrotoxicos)
                .HasForeignKey(a => a.NumeroAreaPlantio);

                //PK AGROTOXICO
                ag.HasOne(a => a.Agrotoxico)
               .WithMany(p => p.ListaAplicarAgrotoxicos)
               .HasForeignKey(a => a.CodigoAgrotoxico);

                //PK PRAGA
                ag.HasOne(a => a.Praga)
               .WithMany(p => p.ListaAplicarAgrotoxicos)
               .HasForeignKey(a => a.CodigoPraga);



            });
        }
    }
}
