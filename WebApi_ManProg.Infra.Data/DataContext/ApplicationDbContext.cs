using Microsoft.EntityFrameworkCore;
using WebApi_ManProg.Domain.Entities;

namespace WebApi_ManProg.Infra.Data.DataContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Mapeando um entidade
    public DbSet<Person> People { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}