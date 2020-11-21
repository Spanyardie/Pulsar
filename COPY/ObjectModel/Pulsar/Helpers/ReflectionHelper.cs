using System;
using System.Reflection;

namespace Pulsar.Helpers
{
    public static class ReflectionHelper
    {
        public static object GetPropValue(object src, string propName)
        {
            object propertyValue = null;

            if (src != null && !string.IsNullOrEmpty(propName))
            {
                Type type = src.GetType();
                if (type != null)
                {
                    PropertyInfo propertyInfo = type.GetProperty(propName);
                    if (propertyInfo != null)
                    {
                        propertyValue = propertyInfo.GetValue(src, null);
                    }
                }
            }
            return propertyValue;
        }
    }
}
