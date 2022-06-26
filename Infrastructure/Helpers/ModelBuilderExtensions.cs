using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
                (
                    new User() { Id = 1, Name = "James", SurName = "Allen" },
                    new User() { Id = 2, Name = "Grant", SurName = "Slaughter" },
                    new User() { Id = 3, Name = "Ronald ", SurName = "Brown" },
                    new User() { Id = 4, Name = "Aaron", SurName = "Lawrence" },
                    new User() { Id = 5, Name = "Randy", SurName = "Greenwood" }
                );

            modelBuilder.Entity<Project>().HasData
                (
                    new Project() { Id = 1, Name = "Sharpen Design", Description = "Has generated over five million design prompts to help you prep for an interview." },
                    new Project() { Id = 2, Name = "Briefz", Description = "For creating a simple and fun brief generator for every kind of designer." },
                    new Project() { Id = 3, Name = "Designercize", Description = "Digital version of an analog whiteboard exercise meant to test your skills as a designer." },
                    new Project() { Id = 4, Name = "The Daily Logo Challenge", Description = "Expect to receive a daily email containing a new logo design challenge to tackle." },
                    new Project() { Id = 5, Name = "Daily UI", Description = " Dribbblers are constantly posting and sharing their work from this challenge." }
                );
        }
    }
}
