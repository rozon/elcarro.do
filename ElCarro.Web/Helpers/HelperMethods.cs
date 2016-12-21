using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ElCarro.Web
{
    public class HelperMethods
    {
        /// <summary>
        /// this method format a string from something like this " Marca Pieza " to "marca_pieza"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatNameString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            // Eliminate the space at the End and Start f the string
            value = value.Trim();
            // Convert the string to lower case
            value = value.ToLowerInvariant();
            // Change all white space with " "
            string pattern = "\\s+";
            string replacement = " ";
            Regex rgx = new Regex(pattern);
            value = rgx.Replace(value, replacement);

            // replace á with a
            value = Replace(value, "[á]", "a");
            // replace é with e
            value = Replace(value, "[é]", "e");
            // replace í with i
            value = Replace(value, "[í]", "i");
            // replace ó with o
            value = Replace(value, "[ó]", "o");
            // replace ú with u
            value = Replace(value, "[ú]", "u");

            return value;
        }

        private static string Replace(string value, string pattern, string replacement)
        {
            // Change all pattern with the replacement
            Regex rgx = new Regex(pattern);
            value = rgx.Replace(value, replacement);

            return value;
        }
    }
}
