using FreestyleDatabase.Shared.Models;
using FreestyleDatabase.Shared.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web;

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

        public static string GetImageOrDefaultWrestler1(this WrestlingDataModel model)
        {
            if (string.IsNullOrEmpty(model.WrestlerImage1))
            {
                model.WrestlerImage1 = $"https://frestyledb.azurewebsites.net/api/FreeStyleImageFetcher?name={HttpUtility.UrlEncode(model.WrestlerName1)}&type=bytes";
            }

            return model.WrestlerImage1;
        }

        public static string GetImageOrDefaultWrestler2(this WrestlingDataModel model)
        {
            if (string.IsNullOrEmpty(model.WrestlerImage2))
            {
                model.WrestlerImage2 = $"https://frestyledb.azurewebsites.net/api/FreeStyleImageFetcher?name={HttpUtility.UrlEncode(model.WrestlerName2)}&type=bytes";
            }

            return model.WrestlerImage2;
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
            try
            {
                model.Country1Emoji = model.GetCountry1Emoji();
                model.FullCountryName1 = model.GetFullCountryName1();
                model.FullCountryName2 = model.GetFullCountryName2();
                model.Country2Emoji = model.GetCountry2Emoji();

                model.Brackets = model.Brackets?.Trim();
                model.Country1 = model.Country1?.Trim();
                model.Country2 = model.Country2?.Trim();
                model.FullCountryName1 = model.FullCountryName1?.Trim();
                model.FullCountryName2 = model.FullCountryName2?.Trim();
                model.Location = model.Location?.Trim();
                model.Result = model.Result?.Trim();
                model.Result2 = model.Result2?.Trim();
                model.Round = model.Round?.Trim();
                model.Score = model.Score?.Trim()?.Replace(" - ", "-");
                model.Venue = model.Venue?.Trim();
                model.Video = model.Video?.Trim();
                model.WeightClass = model.WeightClass?.Trim();
                model.WrestlerName1 = model.GetFixedWrestlerName1()?.Trim();
                model.WrestlerName2 = model.GetFixedWrestlerName2()?.Trim();

                model.WrestlerImage2 = model.GetImageOrDefaultWrestler2()?.Trim();
                model.WrestlerImage1 = model.GetImageOrDefaultWrestler1()?.Trim();
            }
            catch
            {
                // ignored
            }
        }

        public static string AppendToJson(this WrestlingAggregatesModel wrestlingAggregatesModel, string json)
        {
            var asJson = JObject.Parse(json);

            asJson.Add(wrestlingAggregatesModel);

            json = asJson.ToString();

            return json;
        }
    }
}