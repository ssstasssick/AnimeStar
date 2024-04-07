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
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext() : base()
        {
        }

        public DbSet<Anime> Animes { get; set; }
        public DbSet<User> Users { get; set; }
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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AnimeAndCharacter>()
        //        .HasKey(aac => new { aac.AnimeId, aac.CharacterId });

        //    modelBuilder.Entity<AnimeAndCharacter>()
        //        .HasOne(aac => aac.Anime)
        //        .WithMany(anime => anime.AnimeAndCharacters)
        //        .HasForeignKey(aac => aac.AnimeId);

        //    modelBuilder.Entity<AnimeAndCharacter>()
        //        .HasOne(aac => aac.Character)
        //        .WithMany(character => character.AnimeAndCharacters)
        //        .HasForeignKey(aac => aac.CharacterId);

        //    modelBuilder.Entity<AnimeAndGenre>()
        //        .HasKey(aag => new { aag.AnimeId, aag.GenreId });

        //    modelBuilder.Entity<AnimeAndGenre>()
        //        .HasOne(aag => aag.Anime)
        //        .WithMany(anime => anime.AnimeAndGenres)
        //        .HasForeignKey(aag => aag.AnimeId);

        //    modelBuilder.Entity<AnimeAndGenre>()
        //        .HasOne(aag => aag.Genre)
        //        .WithMany(genre => genre.AnimeAndGenres)
        //        .HasForeignKey(aag => aag.GenreId);

        //    modelBuilder.Entity<AnimeAndStudio>()
        //        .HasKey(aas => new { aas.AnimeId, aas.StudioId });

        //    modelBuilder.Entity<AnimeAndStudio>()
        //        .HasOne(aas => aas.Anime)
        //        .WithMany(anime => anime.AnimeAndStudios)
        //        .HasForeignKey(aas => aas.AnimeId);

        //    modelBuilder.Entity<AnimeAndStudio>()
        //        .HasOne(aas => aas.Studio)
        //        .WithMany(studio => studio.AnimeAndStudios)
        //        .HasForeignKey(aas => aas.StudioId);
        //}
    }
}
