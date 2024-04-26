using DAL.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //public ApplicationDbContext() : base()
        //{
        //}

        public DbSet<Anime> Animes { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<PersonalList> PersonalLists { get; set; }
        public DbSet<MPAA> MPAAs { get; set; }
        public DbSet<AnimeAndCharacter> AnimeAndCharacters { get; set; }
        public DbSet<AnimeAndGenre> AnimeAndGenres { get; set; }
        public DbSet<AnimeAndStudio> AnimeAndStudios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = AnimeBase; Integrated Security = True; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Anime>()
                .HasOne(a => a.MPAA)
                .WithMany(mpaa => mpaa.Animes)
                .HasForeignKey(a => a.MPAAId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimeAndCharacter>()
                .HasKey(aac => new { aac.AnimeId, aac.CharacterId });

            modelBuilder.Entity<AnimeAndCharacter>()
                .HasOne(aac => aac.Anime)
                .WithMany(anime => anime.AnimeAndCharacters)
                .HasForeignKey(aac => aac.AnimeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimeAndCharacter>()
                .HasOne(aac => aac.Character)
                .WithMany(character => character.AnimeAndCharacters)
                .HasForeignKey(aac => aac.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimeAndGenre>()
                .HasKey(aag => new { aag.AnimeId, aag.GenreId });

            modelBuilder.Entity<AnimeAndGenre>()
                .HasOne(aag => aag.Anime)
                .WithMany(anime => anime.AnimeAndGenres)
                .HasForeignKey(aag => aag.AnimeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimeAndGenre>()
                .HasOne(aag => aag.Genre)
                .WithMany(genre => genre.AnimeAndGenres)
                .HasForeignKey(aag => aag.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimeAndStudio>()
                .HasKey(aas => new { aas.AnimeId, aas.StudioId });

            modelBuilder.Entity<AnimeAndStudio>()
                .HasOne(aas => aas.Anime)
                .WithMany(anime => anime.AnimeAndStudios)
                .HasForeignKey(aas => aas.AnimeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimeAndStudio>()
                .HasOne(aas => aas.Studio)
                .WithMany(studio => studio.AnimeAndStudios)
                .HasForeignKey(aas => aas.StudioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Forum>()
                .HasOne(f => f.Anime)
                .WithMany(a => a.Forums)
                .HasForeignKey(f => f.AnimeId)
                .OnDelete(DeleteBehavior.Restrict); // Изменено с DeleteBehavior.SetNull на DeleteBehavior.Restrict

            modelBuilder.Entity<Comment>()
                .Property(c => c.ForumId)
                .IsRequired(false);
        }


    }

}
