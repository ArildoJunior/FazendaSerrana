using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class CulturaMap
    {
       

        public CulturaMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cultura>(cultura =>
            {
                cultura.ToTable("TB_CULTURA");
                cultura.HasKey(c => c.Codigo);
                cultura.Property(c => c.Codigo).IsRequired().ValueGeneratedOnAdd();
                cultura.Property(c => c.TempoMaximo).HasColumnType("datetime2");

            });
        }
    }
}
