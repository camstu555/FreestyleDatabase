using System;
using System.Linq;
using rankings2.Models;
using rankings2.Services;

namespace rankings2.Extensions
{
    public static class WrestlingDataModelExtensions
    {
        public static string Country1Emoji(this WrestlingDataModel model)
        {
     
                try
                {
                    var code = CountryCodeConverter.ConvertThreeLetterNameToTwoLetterName(model.Country1);
                    return string.Concat(code.Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
                }
                catch
                {
                    return model.Country1;
                }
            }
  
    }
}
