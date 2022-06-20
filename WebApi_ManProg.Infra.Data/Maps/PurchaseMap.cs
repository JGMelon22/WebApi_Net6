using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Infra.Data.Maps;

public class PurchaseMap : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        # region Mapeando a tabela indicando a PK

        builder.ToTable("compra");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("idcompra")
            .UseIdentityColumn();

        // Atributos e correlacionando as colunas do bd
        builder.Property(x => x.PersonId)
            .HasColumnName("idpessoa")
            .UseIdentityColumn();

        builder.Property(x => x.ProductId)
            .HasColumnName("idproduto")
            .UseIdentityColumn();

        builder.Property(x => x.Date)
            .HasColumnType("date")
            .HasColumnName("datacompra");

        // Uma pessoa tem N compras
        builder.HasOne(x => x.Person)
            .WithMany(y => y.Purchases);

        // Um produto tem N compras
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Purchases);

        # endregion
    }
}