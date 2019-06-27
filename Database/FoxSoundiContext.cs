using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class FoxsoundiContext : DbContext
    {
        public FoxsoundiContext(DbContextOptions<FoxsoundiContext> context) : base(context) { }

        public DbSet<Player> players { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Users");
        }
    }
}
