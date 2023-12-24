using Microsoft.EntityFrameworkCore;
using TaskManagmentSystem.Domain.Entities;
using TaskManagmentSystem.Infrastructure.Configuration;

namespace TaskManagmentSystem.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {

    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=TaskManagmentDB;User Id=SA;Password=Password123;");
        }
    }

    // Define DbSet properties for your entities
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

    }
}
