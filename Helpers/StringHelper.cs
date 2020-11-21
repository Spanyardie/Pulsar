using System.Text.RegularExpressions;

namespace Pulsar.Helpers
{
    public static class StringHelper
    {
        public static string SplitPropertyName(string propertyName)
        {
            return Regex.Replace(propertyName, "([A-Z])", " $1").Trim();
        }
    }
}
