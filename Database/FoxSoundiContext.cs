using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class FoxsoundiContext : DbContext
    {
        public FoxsoundiContext(DbContextOptions<FoxsoundiContext> context) : base(context) { }

        public FoxsoundiContext(DbContextOptions context) : base(context)
        {
            Initializer.Initialize(this);
        }

        public DbSet<Player> players { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Users");
        }
    }
}
