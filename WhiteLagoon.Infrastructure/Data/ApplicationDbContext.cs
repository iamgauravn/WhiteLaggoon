﻿using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        //passing DbContextOptions to the base class
        //dependency injection
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
             
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550
                },

                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 300,
                    Sqft = 550
                },

                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    Sqft = 750
                }

                );

            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    Villa_Number = 101,
                    VillaId = 1,
                },
                new VillaNumber
                {
                    Villa_Number = 104,
                    VillaId = 1,
                },
                new VillaNumber
                {
                    Villa_Number = 105,
                    VillaId = 1,
                },
                new VillaNumber
                {
                    Villa_Number = 201,
                    VillaId = 1,
                },
                new VillaNumber
                {
                    Villa_Number = 203,
                    VillaId = 1,
                },
                new VillaNumber
                {
                    Villa_Number = 301,
                    VillaId = 1,
                },
                new VillaNumber
                {
                    Villa_Number = 302,
                    VillaId = 1,
                }
                );

        }
    }
}
