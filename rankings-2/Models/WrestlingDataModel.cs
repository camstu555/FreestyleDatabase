using System;
using System.Globalization;
using System.Linq;
using rankings2.Services;

namespace rankings2.Models
{
    public class WrestlingDataModel
    {
        public string Country1 { get; set; }

        public string Country1Emoji
        {
            get
            {
               
                try
                {
                    var code = CountryCodeConverter.ConvertThreeLetterNameToTwoLetterName(Country1);
                    return string.Concat(code.Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
                }
                catch (Exception ex) {
                    return ex.Message;
                }
            }
        }

        public string FullCountryName1
        {
            get
            {
                try
                {
                    var code = CountryCodeConverter.ConvertThreeLetterNameToTwoLetterName(Country1);
                    return CountryCodeConverter.TwoLetterNameToDisplayName(code);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string FullCountryName2
        {
            get
            {
                try
                {
                    var code = CountryCodeConverter.ConvertThreeLetterNameToTwoLetterName(Country2);
                    return CountryCodeConverter.TwoLetterNameToDisplayName(code);
                }
                catch (Exception ex)
                {
                    return ex.Message;
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
                    var newName = NameFixer.FirstCharToUpper(lowercase);
                    var fixAmericanName = NameFixer.FixAmerican(newName);

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
                    var code = CountryCodeConverter.ConvertThreeLetterNameToTwoLetterName(Country2);


                    return string.Concat(code.Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
                }
                catch (Exception ex)
                {
                    return ex.Message;
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
                    var newName = NameFixer.FirstCharToUpper(lowercase);
                    return newName;
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
