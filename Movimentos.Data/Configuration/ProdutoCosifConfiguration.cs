using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movimentos.Entities.Entities;

public class ProdutoCosifConfiguration : IEntityTypeConfiguration<ProdutoCosif>
{
    public void Configure(EntityTypeBuilder<ProdutoCosif> builder)
    {
        builder.ToTable("PRODUTO_COSIF");

        builder.HasKey(pc => new { pc.CodigoProduto, pc.CodigoCosif });

        builder.Property(pc => pc.CodigoProduto)
            .HasColumnName("COD_PRODUTO")
            .HasColumnType("char(4)")
            .IsRequired();

        builder.Property(pc => pc.CodigoCosif)
            .HasColumnName("COD_COSIF")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(pc => pc.CodigoClassificacao)
            .HasColumnName("COD_CLASSIFICACAO")
            .HasMaxLength(6);

        builder.Property(pc => pc.Status)
            .HasColumnName("STA_STATUS")
            .HasMaxLength(1);

        builder.HasOne(pc => pc.Produto)
            .WithMany(p => p.ProdutosCosif)
            .HasForeignKey(pc => pc.CodigoProduto);
    }
}
