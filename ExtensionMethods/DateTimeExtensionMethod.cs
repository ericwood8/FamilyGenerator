using System;

namespace FamilyGenerator.ExtensionMethods
{
    public static class DateTimeExtensionMethod
    {
        public static DateTime SubtractYears(this DateTime date, int years)
        {
            return date.AddYears(-years);
        }
    }
}
