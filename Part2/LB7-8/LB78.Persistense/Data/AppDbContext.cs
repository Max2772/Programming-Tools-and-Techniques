using Microsoft.EntityFrameworkCore;

namespace LB78.Persistense.Data;

public class AppDbContext : DbContext
{
    public DbSet<SushiSet> sushiSets { get; set; }
    public DbSet<Sushi> sushi { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SushiSet>()
            .HasMany(s => s.SushiList)
            .WithOne(s => s.SushiSet)
            .HasForeignKey(s => s.SushiSetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
