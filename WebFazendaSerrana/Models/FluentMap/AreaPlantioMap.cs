using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class AreaPlantioMap
    {
      

        public AreaPlantioMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaPlantio>(areaplantio =>
            {
                areaplantio.ToTable("TB_AREAPLANTIO");
                areaplantio.HasKey(a => a.Numero);
                areaplantio.Property(a => a.Numero).IsRequired().ValueGeneratedOnAdd();
                areaplantio.Property(a => a.Status).HasColumnType("nvarchar(15)").IsRequired();

                //PK FUNCIONARIOS
                areaplantio.HasOne(ap => ap.Funcionario)
                .WithMany(f => f.ListaAreaPlantios)
                .HasForeignKey(ap => ap.IdMatricula);
                //PK CULTURA
                areaplantio.HasOne(ap => ap.Cultura)
               .WithMany(c => c.ListaAreaPlantios)
               .HasForeignKey(ap => ap.CodigoCultura);


            });
        }
    }
}
