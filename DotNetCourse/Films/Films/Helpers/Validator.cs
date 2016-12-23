using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Films
{
    internal static class Validator
    {
        public static bool IsNameValidForSearch(string text, out string errorMessage)
        {
            return IsValidForSearch(text, new Regex(@"^[\*?]*[а-яА-Я0-9-!+\.\s]*[\*?]*$"), out errorMessage);
        }

        public static bool IsHumanValidForSearch(string text, out string errorMessage)
        {
            return IsValidForSearch(text, new Regex(@"^[\*?]*[а-яА-Я\s-\.]*[\*?]*$"), out errorMessage);
        }

        public static bool IsCountryValidForSearch(string text, out string errorMessage)
        {
            return IsValidForSearch(text, new Regex(@"^[\*?]*[а-яА-Я-]*[\*?]*$"), out errorMessage);
        }

        public static bool IsYearValidForSearch(string text, out string errorMessage)
        {
            return IsValidForSearch(text, new Regex(@"^[\*?]*[\d]*[\*?]*$"), out errorMessage);
        }

        public static bool IsNameValid(string text, out string errorMessage)
        {
            return IsValid(text, new Regex(@"^[а-яА-Я0-9-!+\.\s]*$"), out errorMessage);
        }

        public static bool IsCountryValid(string text, out string errorMessage)
        {
            return IsValid(text, new Regex(@"^[а-яА-Я-]*$"), out errorMessage);
        }

        public static bool IsYearValid(string text, out string errorMessage)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }

            if (text.Length == 0)
            {
                errorMessage = Properties.Resources.FieldEmpty;
                return false;
            }

            int year;
            if (!int.TryParse(text, out year))
            {
                errorMessage = Properties.Resources.NotANumber;
                return false;
            }

            if (year >= 1896 && year <= DateTime.Now.Year)
            {
                errorMessage = "";
                return true;
            }
            else
            {
                errorMessage = Properties.Resources.InvalidYear;
                return false;
            }
        }

        private static bool IsValidForSearch(String text, Regex regex, out String errorMessage)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }

            bool res = regex.IsMatch(text);
            if (!res)
            {
                errorMessage = Properties.Resources.InvalidChars;
            }
            else
            {
                errorMessage = "";
            }

            return res;
        }

        private static bool IsValid(String text, Regex regex, out String errorMessage)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }

            if (text.Length == 0)
            {
                errorMessage = Properties.Resources.FieldEmpty;
                return false;
            }

            bool res = regex.IsMatch(text);
            if (!res)
            {
                errorMessage = Properties.Resources.InvalidChars;
            }
            else
            {
                errorMessage = "";
            }

            return res;
        }
    }
}
