using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Infra.Data.Maps;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        # region Mapeando a tabela indicando a PK

        builder.ToTable("Produto");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("IdProduto")
            .UseIdentityColumn();

        // Atributos e correlacionando as colunas do bd
        builder.Property(x => x.CodErp)
            .HasColumnName("CodigoErp");

        builder.Property(x => x.Name)
            .HasColumnName("Nome");

        builder.Property(x => x.Price)
            .HasColumnName("Preco");

        // Um produto pode estar em várias compras...
        // mas somente uma compra pode conter ta produto
        builder.HasMany(x => x.Purchases)
            .WithOne(y => y.Product)
            .HasForeignKey(z => z.ProductId);

        # endregion
    }
}