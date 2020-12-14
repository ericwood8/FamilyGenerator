using System;
using System.Collections.Generic;
using System.Linq;
using FamilyGenerator.Enums;
using FamilyGenerator.ExtensionMethods;
using FamilyGenerator.Models;

namespace FamilyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = new DateTime(1345, 1, 1);

            List<City> baseCities = new List<City>()
            {
                new City("Firenze"),
                new City("Genova"),
                new City("Mantua"),
                new City("Milano"),
                new City("Siena"),
                new City("Venezia")
            };

            // do for each of the cities
            foreach (City city in baseCities)
            {
                // create ten FAMILIES
                for (int i = 0; i < 10; i++)
                {
                    Family family = new Family(FamilyTypeEnum.Human);
                    family.CreateFamily(date, city, 18, true); // create family

                    // for each of the random family members
                    foreach (FamilyMember member in family.FamilyMembers)
                    {
                        Console.Write($"{member.Name} {member.Age} ({member.Type.ToDescription()}, {member.LifestyleTraits.First().ToDescription()}), "); // tell name and age and type
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }
    }
}
