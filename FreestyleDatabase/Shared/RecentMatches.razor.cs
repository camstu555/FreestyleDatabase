using FreestyleDatabase.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared
{
    partial class RecentMatches
    {
        [Parameter]
        public List<WrestlingDataModel> Matches { get; set; } = new List<WrestlingDataModel>();

        [Parameter]
        public string CurrentWrestlerId { get; set; } = string.Empty;

        public string Wins
        {
            get
            {
                return $"{Matches.Count(x => x.WrestlerId1.Equals(CurrentWrestlerId))}".PadLeft(2, '0');
            }
        }

        public string Losses
        {
            get
            {
                return $"{Matches.Count(x => !x.WrestlerId1.Equals(CurrentWrestlerId))}".PadLeft(2, '0');
            }
        }
    }
}
