namespace FamilyGenerator.Enums
{
    public enum RelationshipTypeEnum
    {
        BornOf,  // can calc parents, grandparents, and siblings
        CurrentWardOf, // GuardianOf
        FormerWardOf,
        AdoptedChildOf,
        CurrentSpouseOf,
        FormerSpouseOf // can calc half-siblings
    }
}
