using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MvcZoolandia.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if (context.Database == null)
            {
                throw new Exception("DB is null");
            }

            if (context.Animal.Any())
            {
                return;   // DB has been seeded
            }

            context.Animal.AddRange(
                 new Animal
                 {
                     Name = "Donny",
                     IdHabitat = 1,                  
                     IdSpecies = 1
                 },

                 new Animal
                 {
                     Name = "Lenny",
                     IdHabitat = 1,
                     IdSpecies = 2
                 },

                 new Animal
                 {
                     Name = "Freddy",
                     IdHabitat = 1,
                     IdSpecies = 3
                 },

               new Animal
               {
                   Name = "Johnny",
                   IdHabitat = 1,
                   IdSpecies = 2
               }
            );
            context.SaveChanges();
        }
    }
}
