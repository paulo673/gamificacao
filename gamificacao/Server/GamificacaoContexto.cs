using gamificacao.Shared.Models;
using gamificacao.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace gamificacao.Server
{
    public class GamificacaoContexto : DbContext
    {
        private readonly IConfiguration configuration;
        public GamificacaoContexto(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Edicao> Edicoes { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Carta> Cartas { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<VotacaoDeCarta> VotacoesDeCartas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                property.SetColumnType("varchar(250)");
            }

            modelBuilder.Entity<Carta>()
                .Property(x => x.Tipo)
                .HasConversion(x => x.ToString(), x => (TipoDeCarta)Enum.Parse(typeof(TipoDeCarta), x));

            modelBuilder.Entity<Edicao>()
                .Property(x => x.Status)
                .HasConversion(x => x.ToString(), x => (StatusDaEdicao)Enum.Parse(typeof(StatusDaEdicao), x));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("GamificacaoContexto"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
