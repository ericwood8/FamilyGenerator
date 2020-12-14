using System;
using System.Collections.Generic;
using System.Linq;
using FamilyGenerator.Enums;
using FamilyGenerator.ExtensionMethods;
using FamilyGenerator.Helpers;

// 3rd Party
// ours

namespace FamilyGenerator.Models
{ 
    public class Family
    { 
        private FamilyTypeEnum FamilyType { get; }  // required 
        public List<FamilyMember> FamilyMembers { get; } 

        public Family(FamilyTypeEnum familyType) 
        {
            FamilyType = familyType;
            FamilyMembers = new List<FamilyMember>(); // starts off as empty 
        }

        public void CreateFamily(DateTime gameDate, City city, int corePlayerStartingAge, bool isCorePlayerMale)
        {
            if (corePlayerStartingAge < 16)
            {
                throw new ArgumentException("Cannot be younger than 16.");
            }

            DateTime corePlayerBirthDate = gameDate.SubtractYears(corePlayerStartingAge);

            FamilyMember corePlayer = new FamilyMember(FamilyMemberTypeEnum.CoreChild, city, isCorePlayerMale, gameDate, null, false, corePlayerBirthDate, null);
            AddFamilyMember(corePlayer);

            //CurrentHeadOfFamily = corePlayer; 

            FamilyMember father, mother, paternalGrandFather, paternalGrandMother, maternalGrandFather, maternalGrandMother;
            CreateFamilyOfChild(gameDate, city, corePlayer, false, out father, out mother);
            CreateFamilyOfChild(gameDate, city, father, true, out paternalGrandFather, out paternalGrandMother);
            CreateFamilyOfChild(gameDate, city, mother, true, out maternalGrandFather, out maternalGrandMother);
        }

        private void CreateFamilyOfChild(DateTime gameDate, City city, FamilyMember child, bool areDead, out FamilyMember father, out FamilyMember mother)
        {
            const bool isFatherDead = true; // SPECIAL - FOR GAME PURPOSES, I WANT ALWAYS TRUE. Applies to grandfathers as well.
            int? age = FamilyToolbox.CalcFathersAgeNow(child.Age.GetValueOrDefault()); 
            father = new FamilyMember(CalcTypeForFather(child), city, true, gameDate, child.Surname, isFatherDead, null, age);
            AddFamilyMember(father);

            int? mothersAgeNow = areDead ? (int?)null : FamilyToolbox.CalcMothersAgeNow(child.Age.GetValueOrDefault());
            mother = new FamilyMember(CalcTypeForMother(child), city, false, gameDate, null, areDead, null, mothersAgeNow);
            AddFamilyMember(mother);

            RelationshipXref parentSon = new RelationshipXref(RelationshipTypeEnum.BornOf, father, mother, child, null);
            AddRelationship(parentSon);

            if (!areDead)
            {
                // skip calculating marriage date of grandparents
                DateTime marriageDate = FamilyToolbox.CalcMarriageDate(gameDate, mothersAgeNow.GetValueOrDefault(), child.Age.GetValueOrDefault());
                RelationshipXref parentsMarriage = new RelationshipXref(RelationshipTypeEnum.CurrentSpouseOf, father, mother, null, marriageDate);
                AddRelationship(parentsMarriage);
            }

            // Now add siblings
            int numSiblings = FamilyToolbox.CalcNumberOfSiblings();
            if (numSiblings == 0)
            {
                if (child.Type == FamilyMemberTypeEnum.Father)
                {
                    // SPECIAL - FOR GAME PURPOSES, I WANT AT LEAST 1 UNCLE.
                    numSiblings = 1;
                }
                else
                {
                    return; // abort since rest of code deals with siblings or aunts & uncles
                }                
            }

            int paternalUncleCount = 0;
            List<FamilyMember> newFamilyMembers = new List<FamilyMember>();

            for (int i = 0; i < numSiblings; i++)
            {
                // SPECIAL - FOR GAME PURPOSES, I WANT AT LEAST 1 PATERNAL UNCLE AND NO MATERNAL UNCLES.
                bool isMale = ((child.Type == FamilyMemberTypeEnum.Father) && (numSiblings == 1)) || FamilyToolbox.CalcIsMale();

                FamilyMemberTypeEnum sibType = CalcTypeForSibling(child, isMale);

                // SPECIAL - FOR GAME PURPOSES, I WANT ONLY 1 PATERNAL UNCLE AND NO MATERNAL UNCLES.
                if (sibType == FamilyMemberTypeEnum.PaternalUncle)
                {
                    paternalUncleCount++;
                    if (paternalUncleCount > 1)
                    {
                        continue; // skip
                    }
                }
                else if (sibType == FamilyMemberTypeEnum.MaternalUncle)
                {
                    continue; // skip
                }

                FamilyMember sibling = new FamilyMember(sibType, city, isMale, gameDate, child.Surname, false, null, FamilyToolbox.CalcAgeOfSiblings(child.Age.GetValueOrDefault()));
                newFamilyMembers.Add(sibling);

                RelationshipXref siblingBirth = new RelationshipXref(RelationshipTypeEnum.BornOf, father, mother, sibling, null);
                AddRelationship(siblingBirth);
            }

            FamilyMembers.AddRange(newFamilyMembers.OrderByDescending(x => x.Age).ToList()); // sort by age so oldest is first
        }

        private void AddRelationship(RelationshipXref relationship)
        {
            //Relationships.Add(relationship);
        }

        private void AddFamilyMember(FamilyMember familyMember)
        {
            FamilyMembers.Add(familyMember);
        }

        private static FamilyMemberTypeEnum CalcTypeForSibling(FamilyMember child, bool isMale)
        {
            return child.Type switch
            {
                FamilyMemberTypeEnum.Father => (isMale)
                    ? FamilyMemberTypeEnum.PaternalUncle
                    : FamilyMemberTypeEnum.PaternalAunt,
                FamilyMemberTypeEnum.Mother => (isMale)
                    ? FamilyMemberTypeEnum.MaternalUncle
                    : FamilyMemberTypeEnum.MaternalAunt,
                _ => (isMale) ? FamilyMemberTypeEnum.Brother : FamilyMemberTypeEnum.Sister
            };
        }

        private static FamilyMemberTypeEnum CalcTypeForFather(FamilyMember child)
        {
            return child.Type switch
            {
                FamilyMemberTypeEnum.Father => FamilyMemberTypeEnum.PaternalGrandfather,
                FamilyMemberTypeEnum.Mother => FamilyMemberTypeEnum.MaternalGrandfather,
                _ => FamilyMemberTypeEnum.Father
            };
        }
        
        private static FamilyMemberTypeEnum CalcTypeForMother(FamilyMember child)
        {
            return child.Type switch
            {
                FamilyMemberTypeEnum.Father => FamilyMemberTypeEnum.PaternalGrandmother,
                FamilyMemberTypeEnum.Mother => FamilyMemberTypeEnum.MaternalGrandmother,
                _ => FamilyMemberTypeEnum.Mother
            };
        }
    }
}
