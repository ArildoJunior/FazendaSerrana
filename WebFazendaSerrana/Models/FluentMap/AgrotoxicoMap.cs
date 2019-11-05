using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class AgrotoxicoMap
    {
        

        public AgrotoxicoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agrotoxico>(agrotoxico =>
            {
                agrotoxico.ToTable("TB_AGROTOXICO");
                agrotoxico.HasKey(a => a.Codigo);
                agrotoxico.Property(a => a.Codigo).IsRequired().ValueGeneratedOnAdd();


            });
        }
    }
}
