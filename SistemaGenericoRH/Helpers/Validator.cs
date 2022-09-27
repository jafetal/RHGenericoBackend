using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SistemaGenericoRH.Helpers;
namespace SistemaGenericoRH.Helpers
{
    public static class Validator
    {
        public static void ValidateMaxString(String text, int length, String message)
        {

            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            if (text.Length > length)
            {
                throw new GenericException(message);
            }
        }
        public static void ValidateRequired(string text, String message)
        {
            if (text == null)
            {
                throw new GenericException(message);
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new GenericException(message);
            }
        }

        public static void ValidateEmail(String email, String message)
        {

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!Regex.IsMatch(email, pattern))
            {
                throw new GenericException(message);
            }
        }
    }
}
