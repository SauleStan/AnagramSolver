using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class AnagramDbContext : DbContext
    {
        public AnagramDbContext()
        {
        }

        public AnagramDbContext(DbContextOptions<AnagramDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWord> CachedWords { get; set; } = null!;
        public virtual DbSet<SearchInfo> SearchInfos { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWord>(entity =>
            {
                entity.ToTable("CachedWord");

                entity.HasIndex(e => e.AnagramWordId, "IX_CachedWord_AnagramWordId");

                entity.HasIndex(e => e.InputWord, "IX_CachedWord_InputWord");

                entity.Property(e => e.InputWord).HasMaxLength(50);

                entity.HasOne(d => d.AnagramWord)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.AnagramWordId)
                    .HasConstraintName("FK__CachedWor__Anagr__4BAC3F29");
            });

            modelBuilder.Entity<SearchInfo>(entity =>
            {
                entity.ToTable("SearchInfo");

                entity.Property(e => e.SearchedWord).HasMaxLength(50);

                entity.Property(e => e.UserIp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Anagram)
                    .WithMany(p => p.SearchInfos)
                    .HasForeignKey(d => d.AnagramId)
                    .HasConstraintName("FK__SearchInf__Anagr__4E88ABD4");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("Word");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
