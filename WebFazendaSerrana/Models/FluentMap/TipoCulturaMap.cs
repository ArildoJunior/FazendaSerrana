using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class TipoCulturaMap
    {
       

        public TipoCulturaMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoCultura>(tipo =>
            {
                tipo.ToTable("TB_TIPOCULTURA");
                tipo.HasKey(t => new { t.CodigoCultura, t.CodigoPraga });


                //PK CULTURA
                tipo.HasOne(t => t.Cultura)
                .WithMany(c => c.ListaTipoCulturas)
                .HasForeignKey(t => t.CodigoCultura);
                //PK PRAGA
                tipo.HasOne(t => t.Praga)
                .WithMany(p => p.ListaTipoCulturas)
                .HasForeignKey(t => t.CodigoPraga);



            });
        }
    }
}
