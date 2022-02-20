// Estamos mapeando pois o nome usado nas entidades são em ingles
// e os utilizados no banco em portugues

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Infra.Data.Maps;

public class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        # region Mapeando a tabela indicando a PK

        builder.ToTable("Pessoa");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("IdPessoa")
            .UseIdentityColumn();

        // Atributos e correlacionando as colunas do bd

        builder.Property(x => x.Document)
            .HasColumnName("Documento");

        builder.Property(x => x.Name)
            .HasColumnName("Nome");

        builder.Property(x => x.Phone)
            .HasColumnName("Celular");

        // Mapeando as FK
        /*Uma pessoa pode ter uma lista de compras...
        Mas apenas uma compra (a mesma compra) esta relacionada a uma pessoa */
        builder.HasMany(x => x.Purchases)
            .WithOne(y => y.Person)
            .HasForeignKey(x => x.PersonId);

        # endregion
    }
}