using System;

namespace Flaschenpost.Shared
{
    public static class CommonExtensions
    {

        public static T GuardAgainstNull<T>(this T @this, string parameterName)
        where T : class
        {
            if (@this is null)
                throw new ArgumentNullException(parameterName);

            return @this;
        }

        public static bool IsEqualTo(this string source, string value)
        {
            return
            (string.IsNullOrWhiteSpace(source)
            && string.IsNullOrWhiteSpace(value))
            || source.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
    }


}
