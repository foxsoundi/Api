using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class FoxsoundiContext : DbContext
    {
        public FoxsoundiContext(DbContextOptions<FoxsoundiContext> context) : base(context) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlayerFavouritePlaylist> PlayerFavouritePlaylists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Users");
            modelBuilder.Entity<PlayerFavouritePlaylist>().HasKey(pFp => new {pFp.PlayerId, pFp.FavouritePlaylistId});
            modelBuilder.Entity<PlayerFavouritePlaylist>()
                .HasOne(pfp => pfp.Player)
                .WithMany(pfp => pfp.FavouritePlaylists)
                .HasForeignKey(pfp => pfp.PlayerId);
            modelBuilder.Entity<PlayerFavouritePlaylist>()
                .HasOne(pfp => pfp.FavouritePlaylist)
                .WithMany(pfp => pfp.FavouriteBy)
                .HasForeignKey(pfp => pfp.FavouritePlaylistId);

        }
    }
}
