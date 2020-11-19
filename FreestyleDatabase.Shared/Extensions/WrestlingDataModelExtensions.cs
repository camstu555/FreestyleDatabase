using FreestyleDatabase.Shared.Models;
using FreestyleDatabase.Shared.Services;
using System;
using System.Linq;

namespace FreestyleDatabase.Shared.Extensions
{
    public static class WrestlingDataModelExtensions
    {
        public static string GetFixedWrestlerName2(this WrestlingDataModel model)
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

        public static string GetCountry1Emoji(this WrestlingDataModel model)
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

        public static string GetFullCountryName1(this WrestlingDataModel model)
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

        public static string GetFullCountryName2(this WrestlingDataModel model)
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

        public static string GetFixedWrestlerName1(this WrestlingDataModel model)
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

        public static string GetCountry2Emoji(this WrestlingDataModel model)
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

        public static void ApplyMetaData(this WrestlingDataModel model)
        {
            model.FixedWrestlerName2 = model.GetFixedWrestlerName2();
            model.Country1Emoji = model.GetCountry1Emoji();
            model.FullCountryName1 = model.GetFullCountryName1();
            model.FullCountryName2 = model.GetFullCountryName2();
            model.FixedWrestlerName1 = model.GetFixedWrestlerName1();
            model.Country2Emoji = model.GetCountry2Emoji();
        }
    }
}