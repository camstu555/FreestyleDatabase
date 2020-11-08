using System;
using System.Globalization;
using System.Collections.Generic;

namespace rankings2.Services
{
    public static class NameFixer
    {
        public static string FirstCharToUpper(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.  
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.  
            // ... Uppercase the lowercase letters following spaces.  
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static string FixAmerican(string newAmericanName)

        {
            Dictionary<string, string> americans = new Dictionary<string, string>()
        {
            {"Jordan Ernest Burroughs", "Jordan Burroughs"},
            {"Jordan Michael Oliver", "Jordan Oliver"},
            {"Daton Duain Fix", "Daton Fix"},
            {"Tyler Lee Graff", "Tyler Graff"}
        };

              foreach (var pair in americans)
                {
                string name = pair.Key;

                if (newAmericanName.Contains(name))
                {
                    return pair.Value;
                }
               
                }

            return newAmericanName;



        }
    }
}
