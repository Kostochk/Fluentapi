using FluentAPIHw.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FluentAPIHw.Data
{
    public class MoviesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasIndex(u => u.Username).IsUnique();

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.Password)
                      .IsRequired();

                entity.HasMany(u => u.Movies)
                      .WithOne(m => m.User)
                      .HasForeignKey(m => m.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // MOVIE
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Title)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(m => m.Year)
                      .IsRequired();

                entity.Property(m => m.Description)
                      .HasMaxLength(500);

                entity.Property(m => m.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
