using System.ComponentModel;

namespace FamilyGenerator.Enums
{
    public enum FamilyMemberTypeEnum
    {
        [Description("Paternal Grandfather")]
        PaternalGrandfather = 1,

        [Description("Paternal Grandmother")]
        PaternalGrandmother = 2,

        [Description("Maternal Grandmother")]
        MaternalGrandfather = 3,

        [Description("Maternal Grandmother")]
        MaternalGrandmother = 4,

        [Description("Father")]
        Father = 5,

        [Description("Mother")]
        Mother = 6,

        [Description("Paternal Uncle")]
        PaternalUncle = 7,

        [Description("Paternal Aunt")]
        PaternalAunt = 8,

        [Description("Maternal Uncle")]
        MaternalUncle = 9,

        [Description("Maternal Aunt")]
        MaternalAunt = 10,

        [Description("Core Child")]
        CoreChild = 11,

        [Description("Brother")]
        Brother = 12,

        [Description("Sister")]
        Sister = 13
    }
}
