using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FamilyGenerator.ExtensionMethods
{
    public static class EnumExtensionMethod
    {
        public static string ToDescription<TEnum>(this TEnum thisEnum) where TEnum : struct
        {
            return GetEnumDescription((Enum)(object)thisEnum);
        }

        private static Random _R = new Random();
        public static TEnum Random<TEnum>() where TEnum : struct // technically a helper method.
        {
            var v = Enum.GetValues(typeof(TEnum));
            return (TEnum)v.GetValue(_R.Next(v.Length));
        }
         
        public static string RandomString<TEnum>() where TEnum : struct // technically a helper method.
        {
            var v = Enum.GetValues(typeof(TEnum));
            return ((TEnum)v.GetValue(_R.Next(v.Length))).ToString();
        }

        #region Private Methods
        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        #endregion
    }
}
