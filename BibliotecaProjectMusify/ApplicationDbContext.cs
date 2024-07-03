using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaProjectMusify
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Album> Albums { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-767BTASL;database=Musify;trusted_connection=true;Encrypt=False");
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
            .HasMany(a => a.Songs)
            .WithMany(s => s.SongArtists)
            .UsingEntity<Dictionary<string, object>>(
                "ArtistSong",
                j => j
                    .HasOne<Song>()
                    .WithMany()
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Artist>()
                    .WithMany()
                    .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Artist>()
            .HasMany(a => a.Albums)
            .WithOne(album => album.artist)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }*/
    }
}
