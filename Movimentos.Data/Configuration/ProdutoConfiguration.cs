using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movimentos.Entities.Entities;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("PRODUTO");

        builder.HasKey(p => p.CodigoProduto);

        builder.Property(p => p.CodigoProduto)
            .HasColumnName("COD_PRODUTO")
            .HasColumnType("char(4)")
            .IsRequired();

        builder.Property(p => p.Descricao)
            .HasColumnName("DES_PRODUTO")
            .HasMaxLength(30);

        builder.Property(p => p.Status)
            .HasColumnName("STA_STATUS")
            .HasMaxLength(1);
    }
}
