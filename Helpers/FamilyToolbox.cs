using System;
// ours
using FamilyGenerator.ExtensionMethods;

namespace FamilyGenerator.Helpers
{
    public static class FamilyToolbox
    {
        //Function to get random number
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private static int RandomInt(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        } 

        private const int earliestChildbearingAge = 17; // she could be pregnant at 16 + 9 mths to term so say 17
        private const int maxMaleAge = 75;
        private const int maxFemaleAge = 80;
        private const int latestChildbearingAge = 45;
        private const int minYearsToHaveKidAfterMarriage = 2;
        private const int minSiblings = 0; // could be only child
        private const int maxSiblings = 16; 

        public static int CalcFathersAgeNow(int corePlayerStartingAge)
        {
            int earliestFatheringAge = earliestChildbearingAge;
            int maxFatheringAge = maxMaleAge - corePlayerStartingAge;
            if (earliestFatheringAge > maxFatheringAge) 
            {
                maxFatheringAge = maxMaleAge;
            }

            int fathersAgeWhenPlayerBorn = RandomInt(earliestFatheringAge, maxFatheringAge);
            if ((fathersAgeWhenPlayerBorn <= 18) || (fathersAgeWhenPlayerBorn >= 40))
            {
                // reroll to make very young or older fathers less common
                fathersAgeWhenPlayerBorn = RandomInt(earliestFatheringAge, maxFatheringAge);
            }

            if ((fathersAgeWhenPlayerBorn <= 18) || (fathersAgeWhenPlayerBorn >= 60))
            {
                // reroll second time to make very young or very old fathers less common
                fathersAgeWhenPlayerBorn = RandomInt(earliestFatheringAge, maxFatheringAge);
            }

            return fathersAgeWhenPlayerBorn + corePlayerStartingAge + 1;
        }

        public static int CalcMothersAgeNow(int corePlayerStartingAge)
        {
            int earliestMotheringAge = earliestChildbearingAge;
            int maxMotheringAge = Math.Min(maxFemaleAge - corePlayerStartingAge, latestChildbearingAge);

            int mothersAgeWhenPlayerBorn = RandomInt(earliestMotheringAge, maxMotheringAge);
            if (mothersAgeWhenPlayerBorn >= 30)
            {
                // reroll to make very old mothers less common
                mothersAgeWhenPlayerBorn = RandomInt(earliestMotheringAge, maxMotheringAge);
            }

            return mothersAgeWhenPlayerBorn + corePlayerStartingAge + 1;
        }

        public static DateTime CalcMarriageDate(DateTime gameDate, int mothersAgeNow, int corePlayerStartingAge)
        {
            int mothersAgeWhenPlayerBorn = mothersAgeNow - corePlayerStartingAge;
            int mothersAgeWhenMarried = mothersAgeWhenPlayerBorn + RandomInt(minYearsToHaveKidAfterMarriage, mothersAgeNow - earliestChildbearingAge);
            return gameDate.SubtractYears(mothersAgeWhenMarried);
        }

        public static int CalcNumberOfSiblings()
        {
            return RandomInt(minSiblings, maxSiblings);
        }

        public static bool CalcIsMale() // assume 50%/50%
        {
            int i = RandomInt(0, 1000);  // SPECIAL - TESTING SHOWED NON-RANDOM IS TOO CLOSE TOGETHER SUCH AS 0 AND 1
            return (i > 500);
        }

        public static int CalcAgeOfSiblings(int corePersonAge, bool corePersonIsEldest = true)
        {
            // USE CORE PERSON'S AGE ON YOUNGER ONES SO NO ONE WITH NEGATIVE AGE
            int youngestAllowed = -corePersonAge + 1;

            // SPECIAL - In this game, I want core person to be eldest.
            // 0 would be twin, -17 would be 17 yrs younger, 20 would be 20 yrs older
            int roll = RandomInt(youngestAllowed, corePersonIsEldest ? 0 : 20);

            return corePersonAge + roll;
        } 

        /// <summary> WARNING - Approximation!  Just used to get years for integer age. </summary>
        public static int DiffYears(DateTime dateValue1, DateTime dateValue2) 
        {
            var intToCompare1 = Convert.ToInt32(dateValue1.ToString("yyyyMMdd"));
            var intToCompare2 = Convert.ToInt32(dateValue2.ToString("yyyyMMdd"));
            return (intToCompare2 - intToCompare1) / 10000;
        }
    }
}
