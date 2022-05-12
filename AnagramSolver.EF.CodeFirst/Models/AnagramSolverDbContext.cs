using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Models;

public class AnagramSolverDbContext : DbContext
{
    public virtual DbSet<CachedWordEntity> CachedWordEntities { get; set; } = null!;
    public virtual DbSet<SearchInfoEntity> SearchInfoEntities { get; set; } = null!;
    public virtual DbSet<WordEntity> WordEntities { get; set; } = null!;

    public AnagramSolverDbContext(DbContextOptions<AnagramSolverDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CachedWordEntity>(entity =>
        {
            entity.ToTable("CachedWord");
            entity.HasIndex(e => e.InputWord);
            entity.Property(e => e.InputWord)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasOne(d => d.Anagram)
                .WithMany(p => p.CachedWords)
                .HasForeignKey(d => d.AnagramId);
        });

        modelBuilder.Entity<SearchInfoEntity>(entity =>
        {
            entity.ToTable("SearchInfo");

            entity.Property(e => e.SearchedWord)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.UserIp).HasMaxLength(20);

            entity.HasOne(d => d.Anagram)
                .WithMany(p => p.SearchInfos)
                .HasForeignKey(d => d.AnagramId);
        });

        modelBuilder.Entity<WordEntity>(entity =>
        {
            entity.ToTable("Word");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
        });
    }
}