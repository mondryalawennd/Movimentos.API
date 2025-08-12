using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movimentos.Entities.Entities;

public class MovimentoConfiguration : IEntityTypeConfiguration<Movimento>
{
    public void Configure(EntityTypeBuilder<Movimento> builder)
    {
        builder.ToTable("MOVIMENTO_MANUAL");

        builder.HasKey(m => new { m.DataMes, m.DataAno, m.NumeroLancamento });

        builder.Property(m => m.DataMes)
            .HasColumnName("DAT_MES")
            .IsRequired();

        builder.Property(m => m.DataAno)
            .HasColumnName("DAT_ANO")
            .IsRequired();

        builder.Property(m => m.NumeroLancamento)
            .HasColumnName("NUM_LANCAMENTO")
            .IsRequired();

        builder.Property(m => m.CodigoProduto)
            .HasColumnName("COD_PRODUTO")
            .HasColumnType("char(4)")
            .IsRequired();

        builder.Property(m => m.CodigoCosif)
            .HasColumnName("COD_COSIF")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(m => m.Valor)
            .HasColumnName("VAL_VALOR")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(m => m.Descricao)
            .HasColumnName("DES_DESCRICAO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(m => m.DataMovimento)
            .HasColumnName("DAT_MOVIMENTO")
            .IsRequired();

        builder.Property(m => m.CodigoUsuario)
            .HasColumnName("COD_USUARIO")
            .HasMaxLength(15)
            .IsRequired();

        builder.HasOne(m => m.ProdutoCosif)
            .WithMany(pc => pc.Movimentos)
            .HasForeignKey(m => new { m.CodigoProduto, m.CodigoCosif });
    }
}
