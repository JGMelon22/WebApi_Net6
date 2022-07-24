using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Infra.Data.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Usuario");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("IdUsuario");

        // Atributos e correlacionando as colunas do bd
        builder.Property(x => x.Email)
            .HasColumnName("Email");

        builder.Property(x => x.Password)
            .HasColumnName("Senha");
    }
}