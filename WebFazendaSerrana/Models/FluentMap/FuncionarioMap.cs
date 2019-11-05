using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebFazendaSerrana.Models.FluentMap
{
    public class FuncionarioMap
    {
       

        public FuncionarioMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>(funcionario => {
                funcionario.ToTable("TB_FUNCIONARIO");
                funcionario.HasKey(f => f.Matricula);
                funcionario.Property(f => f.Matricula).IsRequired().ValueGeneratedOnAdd();
                funcionario.Property(f => f.DataAdmissao).HasColumnType("datetime2");


            });

        }
    }
}
