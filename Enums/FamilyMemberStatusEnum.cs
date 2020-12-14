using System.ComponentModel;

namespace FamilyGenerator.Enums
{
    public enum FamilyMemberStatusEnum
    {
        [Description("Alive and Free")]
        AliveAndFree = 1,

        [Description("Alive and Imprisoned")]
        AliveAndImprisoned = 2,

        [Description("Alive and Excommunicated")]
        AliveAndExcommunicated = 3,

        [Description("Death by burned at the stake")]
        DeathByBurnedAtStake = 4,

        [Description("Death by poison")]
        DeathByPoison = 5,

        [Description("Death by battle")]
        DeathByBattle = 6, // Champion or Army General or Admiral

        [Description("Death by assassination")]
        DeathByAssassination = 7,

        [Description("Death by age")]
        DeathByAge = 8,

        [Description("Death by imprisionment")]
        DeathByImprisionment = 9
    }
}
