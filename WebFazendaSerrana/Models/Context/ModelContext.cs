using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFazendaSerrana.Models.FluentMap;

namespace WebFazendaSerrana.Models.Context
{
    public class ModelContext : DbContext
    {

        public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            new AgrotoxicoMap(modelBuilder);
            new AplicarAgrotoxicoMap(modelBuilder);
            new AreaPlantioMap(modelBuilder);
            new AuxiliarAgrotoxicoMap(modelBuilder);
            new CulturaMap(modelBuilder);
            new FuncionarioMap(modelBuilder);
            new PragaMap(modelBuilder);
            new TipoCulturaMap(modelBuilder);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WebFazendaSerrana.Models.Agrotoxico> Agrotoxicos { get; set; }
        public DbSet<WebFazendaSerrana.Models.AplicarAgrotoxico> AplicarAgrotoxicos { get; set; }
        public DbSet<WebFazendaSerrana.Models.AreaPlantio> AreaPlantios { get; set; }
        public DbSet<WebFazendaSerrana.Models.AuxiliarAgrotoxico> AuxiliarAgrotoxicos { get; set; }

        public DbSet<WebFazendaSerrana.Models.Cultura> Culturas { get; set; }

        public DbSet<WebFazendaSerrana.Models.Funcionario> Funcionarios { get; set; }

        public DbSet<WebFazendaSerrana.Models.Praga> Pragas { get; set; }
        public DbSet<WebFazendaSerrana.Models.TipoCultura> TipoCulturas { get; set; }

    }
}
