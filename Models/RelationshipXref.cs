using System;
using FamilyGenerator.Enums;
using FamilyGenerator.ExtensionMethods;
// ours

namespace FamilyGenerator.Models
{
    public class RelationshipXref
    {
        public FamilyMember Child { get; }
        public FamilyMember AdultMale { get; }
        public FamilyMember AdultFemale { get; }
        public RelationshipTypeEnum RelationshipType { get; set; }
        public DateTime LastStarted { get; } // could remarry, etc. multiple times, so this would only have last time
        public DateTime? LastEnded { get; set; }

        public RelationshipXref(RelationshipTypeEnum relationshipEvent, FamilyMember adultMale, FamilyMember adultFemale, FamilyMember child = null, DateTime? gameDate = null)
        {
            if (adultMale.Equals(adultFemale))
            {
                throw new Exception($"Cannot have {relationshipEvent.ToDescription()} with self");
            }
            // TODO: Prevent duplicates. Look through existing Xref to see.
            // TODO: Prevent polygamy. Look through existing Xref to see.
            // Remember could remarry former spouse again OR could marry within the family

            RelationshipType = relationshipEvent;
            AdultMale = adultMale;
            AdultFemale = adultFemale;
            if (child != null)  // marriage & divorce do not involve child
            {
                Child = child;
            }

            if (gameDate == null)
            {
                LastStarted = Child.BirthDate.GetValueOrDefault();
            }
            else
            {
                LastStarted = gameDate.GetValueOrDefault();
            }
        }
    }
}
