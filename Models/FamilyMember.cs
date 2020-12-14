using System;
using System.Collections.Generic;
using FamilyGenerator.Enums;
using FamilyGenerator.Enums.Names;
using FamilyGenerator.ExtensionMethods;
using FamilyGenerator.Helpers;

// ours

namespace FamilyGenerator.Models
{
    public class FamilyMember  
    {
        public string Name { get; }
        public DateTime? BirthDate { get; }
        public int? Age { get; }
        private bool IsMale { get; }
        private FamilyMemberStatusEnum FamilyMemberStatus { get; }
        public List<LifestyleTraitsEnum> LifestyleTraits { get; }
        public string Surname { get; }
        public FamilyMemberTypeEnum Type { get; }


        public FamilyMember(FamilyMemberTypeEnum type, City baseCity, bool isMale, DateTime gameDate, string forceSurname, bool isDead, DateTime? birthDate, int? age) 
        {
            Type = type;
            IsMale = isMale;
            FamilyMemberStatus = FamilyMemberStatusEnum.AliveAndFree;
            LifestyleTraits = new List<LifestyleTraitsEnum> { EnumExtensionMethod.Random<LifestyleTraitsEnum>() }; // randomly pick one

            if ((isDead) && (type != FamilyMemberTypeEnum.Father))
            {
                BirthDate = null; 
                Age = null;
            }
            else
            { 
                if (birthDate == null)
                {
                    BirthDate = gameDate.SubtractYears(age.GetValueOrDefault());
                    Age = age.GetValueOrDefault();
                }
                else
                {
                    BirthDate = birthDate.GetValueOrDefault();
                    Age = FamilyToolbox.DiffYears(BirthDate.GetValueOrDefault(), gameDate);
                }
            }

            if (string.IsNullOrWhiteSpace(forceSurname))
            {
                Surname = CalcSurname(baseCity); // generate random one for focal person
            }
            else
            {
                Surname = forceSurname; // we want father's and siblings surnames to match focal person's 
            }

            Name = CalcFirstName(baseCity, isMale) + " " + Surname;
        } 

        private static string CalcFirstName(City baseCity, bool isMale)
        {
            if (isMale)
            {
                return baseCity.Name switch
                {
                    "Firenze" => EnumExtensionMethod.RandomString<FirenzeMalesEnum>(), // a.k.a. Florence 
                    "Genova" => EnumExtensionMethod.RandomString<GenovaMalesEnum>(), // a.k.a. Genoa,
                    "Mantua" => EnumExtensionMethod.RandomString<MantuaMalesEnum>(), // a.k.a. Mantua,
                    "Milano" => EnumExtensionMethod.RandomString<MilanoMalesEnum>(), // a.k.a. Milan,
                    "Siena" => EnumExtensionMethod.RandomString<SienaMalesEnum>(), // a.k.a. Siena,
                    "Venezia" => EnumExtensionMethod.RandomString<VeneziaMalesEnum>(), // a.k.a. Venice,
                    _ => throw new Exception("Names for city not prepared.")
                };
            } 
            else
            {
                return baseCity.Name switch
                {
                    "Firenze" => EnumExtensionMethod.RandomString<FirenzeFemalesEnum>(), // a.k.a. Florence,
                    "Genova" => EnumExtensionMethod.RandomString<VeneziaFemalesEnum>(), // a.k.a. Genoa. SPECIAL - REUSE VENETIAN FEMALE.  COULD NOT FIND ENOUGH FEMALE NAMES TO DO A PROPER ENUM.
                    "Mantua" => EnumExtensionMethod.RandomString<MantuaFemalesEnum>(), // a.k.a. Mantua
                    "Milano" => EnumExtensionMethod.RandomString<MilanoFemalesEnum>(), // a.k.a. Milan
                    "Siena" => EnumExtensionMethod.RandomString<SienaFemalesEnum>(), // a.k.a. Siena
                    "Venezia" => EnumExtensionMethod.RandomString<VeneziaFemalesEnum>(), // a.k.a. Venice
                    _ => throw new Exception("Names for city not prepared.")
                };
            }
        }

        private static string CalcSurname(City baseCity)
        {
            return baseCity.Name switch
            {
                "Firenze" => EnumExtensionMethod.RandomString<FirenzeSurnamesEnum>(), // a.k.a. Florence
                "Genova" => EnumExtensionMethod.RandomString<GenovaSurnamesEnum>(), // a.k.a. Genoa,
                "Mantua" => EnumExtensionMethod.RandomString<MantuaSurnamesEnum>(), // a.k.a. Mantua,
                "Milano" => EnumExtensionMethod.RandomString<MilanoSurnamesEnum>(), // a.k.a. Milan,
                "Siena" => EnumExtensionMethod.RandomString<SienaSurnamesEnum>(), // a.k.a. Siena,
                "Venezia" => EnumExtensionMethod.RandomString<VeneziaSurnamesEnum>(), // a.k.a. Venice,
                _ => throw new Exception("Names for city not prepared.")
            };
        } 
    }
}
