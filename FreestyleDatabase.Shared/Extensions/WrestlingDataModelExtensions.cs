using FreestyleDatabase.Shared.Models;
using FreestyleDatabase.Shared.Services;
using System;
using System.Linq;

namespace FreestyleDatabase.Shared.Extensions
{
    public static class WrestlingDataModelExtensions
    {
        public static string FixedWrestlerName2(this WrestlingDataModel model)
        {
            try
            {
                var lowercase = model.WrestlerName2.ToLower();
                var newName = NameFixerExtensions.FirstCharToUpper(lowercase);
                var fixAmericanName = NameFixerExtensions.FixAmericanName(newName);

                return fixAmericanName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Country1Emoji(this WrestlingDataModel model)
        {
            try
            {
                var code = model.Country1.ConvertThreeLetterNameToTwoLetterName();
                return string.Concat(code.Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
            }
            catch
            {
                return model.Country1;
            }
        }

        public static string FullCountryName1(this WrestlingDataModel model)
        {
            try
            {
                var code = model.Country1.ConvertThreeLetterNameToTwoLetterName();
                return code.TwoLetterNameToDisplayName();
            }
            catch
            {
                return model.Country1;
            }
        }

        public static string FullCountryName2(this WrestlingDataModel model)
        {
            try
            {
                var code = CountryCodeConverterExtensions.ConvertThreeLetterNameToTwoLetterName(model.Country2);
                return CountryCodeConverterExtensions.TwoLetterNameToDisplayName(code);
            }
            catch
            {
                return model.Country2;
            }
        }

        public static string FixedWrestlerName1(this WrestlingDataModel model)
        {
            try
            {
                var lowercase = model.WrestlerName1.ToLower();
                var newName = NameFixerExtensions.FirstCharToUpper(lowercase);
                var fixAmericanName = NameFixerExtensions.FixAmericanName(newName);

                return fixAmericanName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Country2Emoji(this WrestlingDataModel model)
        {
            try
            {
                var code = CountryCodeConverterExtensions.ConvertThreeLetterNameToTwoLetterName(model.Country2);

                return string.Concat(code.Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
            }
            catch
            {
                return model.Country2;
            }
        }
    }
}