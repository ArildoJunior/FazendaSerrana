using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class PragaMap
    {


        public PragaMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Praga>(praga =>
            {
                praga.ToTable("TB_PRAGA");
                praga.HasKey(p => p.Codigo);
                praga.Property(p => p.Codigo).IsRequired().ValueGeneratedOnAdd();
                praga.Property(p => p.DataUltimaPraga).HasColumnType("datetime2");



            });
        }
    }
}
