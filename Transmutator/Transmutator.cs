using System;
using System.Globalization;

namespace KissTools
{
    public static class Transmutator
    {
        public static T ConvertType<T>(object obj)
        {
            return ConvertType<T>(obj, CultureInfo.InvariantCulture);
        }

        public static T ConvertType<T>(object obj, IFormatProvider formatProvider)
        {
            return (T)Convert.ChangeType(obj, typeof(T), formatProvider);
        }
    }
}
