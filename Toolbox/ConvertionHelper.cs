using System;
using System.Globalization;

namespace Toolbox
{
    public class ConvertionHelper
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
