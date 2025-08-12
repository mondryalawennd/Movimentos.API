using Microsoft.EntityFrameworkCore;
using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;
using System;

namespace Movimentos.Data.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoCosif> ProdutoCosifs { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<MovimentoManualDTO> MovimentosManuaisDTO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoCosifConfiguration());
            modelBuilder.ApplyConfiguration(new MovimentoConfiguration());

            modelBuilder.Entity<MovimentoManualDTO>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.Mes).HasColumnName("Mes").HasColumnType("int");
                entity.Property(e => e.Ano).HasColumnName("Ano").HasColumnType("int");
                entity.Property(e => e.CodigoProduto).HasColumnName("CodigoProduto");
                entity.Property(e => e.DescricaoProduto).HasColumnName("DescricaoProduto");
                entity.Property(e => e.NumeroLancamento).HasColumnName("NumeroLancamento").HasColumnType("bigint");
                entity.Property(e => e.DescricaoMovimento).HasColumnName("DescricaoMovimento");
                entity.Property(e => e.ValorMovimento).HasColumnName("ValorMovimento");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
