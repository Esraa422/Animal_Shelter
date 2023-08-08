using Animal_Shelter.Models;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Animal_Shelter.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animalpic_Animal>().HasKey(am => new
            {
                am.AnimalpicId,
                am.AnimalId
            });
            modelBuilder.Entity<Animalpic_Animal>().HasOne(m => m.Animal).WithMany(am => am.Animalpic_Animal).HasForeignKey(m => m.AnimalId);

            modelBuilder.Entity<Animalpic_Animal>().HasOne(m => m.Animalpic).WithMany(am => am.Animalpic_Animal).HasForeignKey(m => m.AnimalpicId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Animalpic> Animalpics { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Animalpic_Animal> Animalpics_Animals { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Nationality> Nationality { get; set; }
        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}