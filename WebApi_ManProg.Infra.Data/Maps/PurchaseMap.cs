using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Infra.Data.Maps;

public class PurchaseMap : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        # region Mapeando a tabela indicando a PK

        builder.ToTable("Compra");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("IdCompra")
            .UseIdentityColumn();

        // Atributos e correlacionando as colunas do bd
        builder.Property(x => x.PersonId)
            .HasColumnName("IdPessoa")
            .UseIdentityColumn();

        builder.Property(x => x.ProductId)
            .HasColumnName("IdProduto")
            .UseIdentityColumn();

        builder.Property(x => x.Date)
            .HasColumnName("DataCompra");

        // Uma pessoa tem N compras
        builder.HasOne(x => x.Person)
            .WithMany(y => y.Purchases);

        // Um produto tem N compras
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Purchases);

        # endregion
    }
}