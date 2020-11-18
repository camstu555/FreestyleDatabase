using FreestyleDatabase.Shared.Services;
using System;
using System.Linq;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingDataModel
    {
        public string Country1 { get; set; }

        public string FullCountryName1
        {
            get
            {
                try
                {
                    var code = Country1.ConvertThreeLetterNameToTwoLetterName();
                    return code.TwoLetterNameToDisplayName();
                }
                catch
                {
                    return Country1;
                }
            }
        }

        public string FullCountryName2
        {
            get
            {
                try
                {
                    var code = CountryCodeConverterExtensions.ConvertThreeLetterNameToTwoLetterName(Country2);
                    return CountryCodeConverterExtensions.TwoLetterNameToDisplayName(code);
                }
                catch
                {
                    return Country2;
                }
            }
        }

        public string WeightClass { get; set; }

        public string WrestlerName1 { get; set; }

        public string FixedWrestlerName1
        {
            get
            {
                try
                {
                    var lowercase = WrestlerName1.ToLower();
                    var newName = NameFixerExtensions.FirstCharToUpper(lowercase);
                    var fixAmericanName = NameFixerExtensions.FixAmericanName(newName);

                    return fixAmericanName;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string Result { get; set; }

        public string Score { get; set; }

        public string Country2 { get; set; }

        public string Country2Emoji
        {
            get
            {
                try
                {
                    var code = CountryCodeConverterExtensions.ConvertThreeLetterNameToTwoLetterName(Country2);

                    return string.Concat(code.Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
                }
                catch
                {
                    return Country2;
                }
            }
        }

        public string WrestlerName2 { get; set; }

        public string FixedWrestlerName2
        {
            get
            {
                try
                {
                    var lowercase = WrestlerName2.ToLower();
                    var newName = NameFixerExtensions.FirstCharToUpper(lowercase);
                    var fixAmericanName = NameFixerExtensions.FixAmericanName(newName);

                    return fixAmericanName;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string Result2 { get; set; }

        public string Date { get; set; }

        public string Venue { get; set; }

        public string Location { get; set; }

        public string Round { get; set; }

        public string Video { get; set; }

        public string Brackets { get; set; }
    }
}