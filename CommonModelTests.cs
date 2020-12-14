using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FamilyGenerator.Enums;
using FamilyGenerator.ExtensionMethods;
using FamilyGenerator.Models;

namespace ModelsTest
{

    [TestClass]
    public class CommonModelTests
    {
        [TestMethod]
        public void TestMaleAndFemaleNamesForAllCities()
        {
            DateTime date = new DateTime(1345, 1, 1);
            DateTime birthDate = new DateTime(1325, 1, 1);

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
                Console.WriteLine();
                Console.WriteLine("City of " + city.Name + ":");
                Console.Write("Male Names: ");

                // create ten MALE core children for main character
                for (int i = 0; i < 10; i++)
                {
                    FamilyMember familyMember = new FamilyMember(FamilyMemberTypeEnum.CoreChild, city, true, date, null, false, birthDate, null);
                    Console.Write(familyMember.Name + ", "); // write out main character's name
                }

                Console.WriteLine();
                Console.Write("Female Names: ");

                // create ten FEMALE core children for main character
                for (int i = 0; i < 10; i++)
                {
                    FamilyMember familyMember = new FamilyMember(FamilyMemberTypeEnum.CoreChild, city, false, date, null, false, birthDate, null);
                    Console.Write(familyMember.Name + ", "); // write out main character's name
                }

                Console.WriteLine();
            }
        } 

        [TestMethod]
        public void TestCreateTenFamiliesPerCity()
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
                // create ten test FAMILIES
                for (int i = 0; i < 10; i++)
                {
                    Family family = new Family(FamilyTypeEnum.Human);
                    family.CreateFamily(date, city, 18, true); // create family

                    // for each of the random family members
                    foreach (FamilyMember member in family.FamilyMembers)
                    {
                        Console.Write(member.Name + " " + member.Age.ToString() + " (" + member.Type.ToDescription() + "), "); // tell name and age and type
                    }

                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }
    }
}
    