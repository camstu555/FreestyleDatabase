using System;
using System.Globalization;

namespace FreestyleDatabase.Shared.Services
{
    public static class CountryCodeConverterExtensions
    {
        public static string ConvertThreeLetterNameToTwoLetterName(this string threeLetterCountryCode)
        {
            if (threeLetterCountryCode == null || threeLetterCountryCode.Length != 3)
            {
                throw new ArgumentException("name must be three letters.");
            }

            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var culture in cultures)
            {
                var region = new RegionInfo(culture.LCID);

                switch (threeLetterCountryCode)
                {
                    case "IRI":
                        threeLetterCountryCode = "IRN";
                        break;

                    case "MGL":
                        threeLetterCountryCode = "MNG";
                        break;

                    case "ALG":
                        threeLetterCountryCode = "DZA";
                        break;

                    case "GER":
                        threeLetterCountryCode = "DEU";
                        break;

                    case "PHI":
                        threeLetterCountryCode = "PHL";
                        break;

                    case "HON":
                        threeLetterCountryCode = "HND";
                        break;

                    case "RSA":
                        threeLetterCountryCode = "ZAF";
                        break;

                    case "NED":
                        threeLetterCountryCode = "NLD";
                        break;

                    case "SLO":
                        threeLetterCountryCode = "SVN";
                        break;

                    case "BUL":
                        threeLetterCountryCode = "BGR";
                        break;

                    case "CRC":
                        threeLetterCountryCode = "CRI";
                        break;

                    case "PUR":
                        threeLetterCountryCode = "PRI";
                        break;

                    case "GRE":
                        threeLetterCountryCode = "GRC";
                        break;

                    case "NGR":
                        threeLetterCountryCode = "NGA";
                        break;

                    case "SUD":
                        threeLetterCountryCode = "SDN";
                        break;

                    case "SUI":
                        threeLetterCountryCode = "CHE";
                        break;

                    case "ASA":
                        threeLetterCountryCode = "ASM";
                        break;

                    case "KOS":
                        threeLetterCountryCode = "XXK";
                        break;
                }

                if (region.ThreeLetterISORegionName.ToUpper() == threeLetterCountryCode.ToUpper())
                {
                    return region.TwoLetterISORegionName;
                }

                switch (threeLetterCountryCode)
                {
                    case "PRK":
                        return "KP";

                    case "FSM":
                        return "FM";

                    case "GBS":
                        return "GW";

                    case "GUI":
                        return "GN";

                    case "NRU":
                        return "NR";

                    case "SMR":
                        return "SM";

                    case "BDI":
                        return "BI";

                    case "MAD":
                        return "MG";

                    case "CHA":
                        return "TD";

                    case "PLW":
                        return "PW";

                    case "GUM":
                        return "GU";

                    case "UGA":
                        return "UG";
                }
            }

            throw new ArgumentException("Could not get country code");
        }

        public static string TwoLetterNameToDisplayName(this string twoLetterCountryCode)
        {
            if (twoLetterCountryCode == null || twoLetterCountryCode.Length != 2)
            {
                throw new ArgumentException("name must be two letters.");
            }

            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var culture in cultures)
            {
                var region = new RegionInfo(culture.LCID);

                if (region.TwoLetterISORegionName.ToUpper() == twoLetterCountryCode.ToUpper())
                {
                    return region.DisplayName;
                }
            }

            throw new ArgumentException("Could not get country code");
        }
    }
}