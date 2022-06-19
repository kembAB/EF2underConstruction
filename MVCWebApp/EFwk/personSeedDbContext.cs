using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebApp.Models.Person;
using MVCWebApp.Models.City;
using MVCWebApp.Models.Country;

namespace MVCWebApp.EFwk
{
    public class personSeedDbContext:DbContext
    {
        public personSeedDbContext(DbContextOptions<personSeedDbContext> options) : base(options)
        {

        }

        public DbSet<personproperties> People { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<City>()
                .Property<string>("CountryForeignKey");
            modelBuilder.Entity<personproperties>()
                .Property<int>("CityForeignKey");

         
            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(co => co.Cities)
            .HasForeignKey("CountryForeignKey");

            modelBuilder.Entity<personproperties>()
                .HasOne(p => p.City)
                .WithMany(c => c.People)
            .HasForeignKey("CityForeignKey");

         
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryName = "Sweden" },
                new Country { CountryName = "USA" },
                new Country { CountryName = "UK" });

         
            modelBuilder.Entity<City>().HasData(
            new { ID = 1, CityName = "Lund", CountryForeignKey = "Sweden" },
            new { ID = 2, CityName = "Gothenburg", CountryForeignKey = "USA" },
            new { ID = 3, CityName = "Stockholm", CountryForeignKey = "UK" });

            
            modelBuilder.Entity<personproperties>().HasData(
                new { ID = 1, Name = "John Stwart", PhoneNumber = "0786574567", CityForeignKey = 1 },
                new { ID = 2, Name = "Josefine Gustafsson", PhoneNumber = "0786544567", CityForeignKey = 2 },
                new { ID = 3, Name = "Andrew  Monnet", PhoneNumber = "0786894567", CityForeignKey = 3 });

            //  modelBuilder.Entity<personproperties>().HasData(new personproperties { ID = 1, Name = "John Stwart", City = "Lund", PhoneNumber = "0786574567" });
           // modelBuilder.Entity<personproperties>().HasData(new personproperties { ID = 2, Name = "Josefine Gustafsson", City = "Gothenburg", PhoneNumber = "0786544567" });
           // modelBuilder.Entity<personproperties>().HasData(new personproperties { ID = 3, Name = "Andrew  Monnet", City = "Stockholm", PhoneNumber = "0786894567" });

        }
    }
}

