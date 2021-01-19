using FreestyleDatabase.Shared.Extensions;
using FreestyleDatabase.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared.Services
{
    public class WrestlingDataService
    {
        private readonly GoogleSheetService googleSheetService;

        public WrestlingDataService(GoogleSheetService googleSheetService)
        {
            this.googleSheetService = googleSheetService;
        }

        public async Task<List<WrestlingDataModel>> SearchWrestlerDataAsync(string name = null, string country = null, string emoji = null, string weight = null, string tournament = null, string opponent = null)
        {
            var results = await GetWrestlerDataAsync();

            if (name != null)
            {
                results = results
                    .Where(d => d.WrestlerName1.Contains(name, StringComparison.CurrentCultureIgnoreCase) || d.WrestlerName2.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();

                if (opponent != null)
                {
                    results = results
                        .Where(d => d.WrestlerName1.Contains(opponent, StringComparison.CurrentCultureIgnoreCase) || d.WrestlerName2.Contains(opponent, StringComparison.CurrentCultureIgnoreCase))
                        .ToList();
                }
            }

            if (country != null)
            {
                results = results
                    .Where(d => d.Country1.Contains(country, StringComparison.CurrentCultureIgnoreCase) || d.Country2.Contains(country, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            if (emoji != null)
            {
                results = results
                    .Where(d => d.Country1Emoji.Contains(emoji, StringComparison.CurrentCultureIgnoreCase) || d.Country2Emoji.Contains(emoji, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            if (weight != null)
            {
                results = results
                    .Where(d => d.WeightClass.Contains(weight, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            if (tournament != null)
            {
                results = results
                    .Where(d => d.Venue.Contains(tournament, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            return results;
        }

        public async Task<List<WrestlingDataModel>> GetWrestlerDataAsync()
        {
            var results = new List<WrestlingDataModel>();
            var googleData = await googleSheetService.GetSheetAsync();

            if (googleData == null)
            {
                return results;
            }

            for (int i = 0; i < googleData.Feed.Entry.Count; i++)
            {
                var data = googleData.Feed.Entry[i];

                DateTimeOffset? date = null;

                if (DateTimeOffset.TryParse(data.GsxDate.Value, out var parsedDate))
                {
                    date = parsedDate;
                }

                var newData = new WrestlingDataModel
                {
                    Country1 = data.GsxCountry.Value,
                    Country2 = data.GsxCountry2.Value,
                    Date = date,
                    WrestlerName1 = data.GsxName.Value,
                    WrestlerName2 = data.GsxName2.Value,
                    WeightClass = data.GsxWeightClass.Value,
                    Venue = data.GsxTournament.Value,
                    Location = data.GsxLocation.Value,
                    Result = data.GsxResult.Value,
                    Round = data.GsxRound.Value,
                    Score = data.GsxScore.Value,
                    Video = data.GsxVideo.Value,
                    Brackets = data.GsxBracket.Value,
                };

                newData.ApplyMetaData(i);

                results.Add(newData);
            }

            return results;
        }
    }
}