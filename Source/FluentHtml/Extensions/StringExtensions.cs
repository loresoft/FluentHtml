using System;

namespace FluentHtml.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates the specified text.
        /// </summary>
        /// <param name="text">The text to truncate.</param>
        /// <param name="keep">The number of characters to keep.</param>
        /// <param name="ellipsis">The ellipsis string to use when truncating. (Default ...)</param>
        /// <returns>
        /// A truncate string.
        /// </returns>
        public static string Truncate(this string text, int keep, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (string.IsNullOrEmpty(ellipsis))
                ellipsis = string.Empty;

            string buffer = text;
            if (buffer.Length <= keep)
                return buffer;

            if (buffer.Length <= keep + ellipsis.Length || keep < ellipsis.Length)
                return buffer.Substring(0, keep);

            return string.Concat(buffer.Substring(0, keep - ellipsis.Length), ellipsis);
        }

        /// <summary>
        /// Indicates whether the specified String object is null or an empty string
        /// </summary>
        /// <param name="item">A String reference</param>
        /// <returns>
        ///     <c>true</c> if is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters
        /// </summary>
        /// <param name="item">A String reference</param>
        /// <returns>
        ///      <c>true</c> if is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string item)
        {
            if (item == null)
                return true;

            for (int i = 0; i < item.Length; i++)
                if (!char.IsWhiteSpace(item[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Determines whether the specified string is not <see cref="IsNullOrEmpty"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="value"/> is not <see cref="IsNullOrEmpty"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Uses the string as a format
        /// </summary>
        /// <param name="format">A String reference</param>
        /// <param name="args">Object parameters that should be formatted</param>
        /// <returns>Formatted string</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(format, args);
        }
    }
}