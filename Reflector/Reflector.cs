using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Globalization;

namespace KissTools
{
    public static class Reflector
    {
        public static String[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties().Select(p => p.Name).ToArray();
        }

        public static String GetPropertyName<T>(Expression<Func<T, object>> prop)
        {
            MemberExpression body = prop.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)prop.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        public static void SetValue(object obj, String propName, object propValue, bool forceConversion)
        {
            SetValue(obj, GetProperty(obj, propName), propValue, forceConversion);
        }

        public static object GetValue(object obj, String propName)
        {
            return GetValue(obj, GetProperty(obj, propName));
        }

        public static String GetBestMatchProperty(object targetObj, String sourcePropName, ReflectorOption options)
        {
            foreach (String targetProp in GetProperties(targetObj))
            {
                String targetPropName = targetProp;
                if ((options & ReflectorOption.IGNORE_CASE) == ReflectorOption.IGNORE_CASE)
                {
                    targetPropName = targetPropName.ToLower();
                    sourcePropName = sourcePropName.ToLower();
                }
                if ((options & ReflectorOption.IGNORE_UNDERSCORE) == ReflectorOption.IGNORE_UNDERSCORE)
                {
                    targetPropName = targetPropName.Replace("_", "");
                    sourcePropName = sourcePropName.Replace("_", "");
                }
                if (targetPropName == sourcePropName) return targetProp;
            }
            return null;
        }

        private static PropertyInfo GetProperty(object obj, String propName)
        {
            if (propName == null || obj == null) return null;
            return obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
        }

        private static object GetValue(object obj, PropertyInfo propInfo)
        {
            if (propInfo == null || obj == null) return null;
            return propInfo.GetValue(obj);
        }

        private static void SetValue(object obj, PropertyInfo propInfo, object value, bool forceConversion)
        {
            try
            {
                if (propInfo == null || obj == null) return;
                if (value != null && forceConversion) value = Convert.ChangeType(value, propInfo.PropertyType, CultureInfo.InvariantCulture);
                if (propInfo != null && propInfo.CanWrite) propInfo.SetValue(obj, value);
            }
            catch (Exception ex) when (ex is FormatException | ex is ArgumentException)
            {
                throw new InvalidCastException($"Unable to convert {value.GetType()} to {propInfo.PropertyType}");
            }
            
        }

    }
}
