using FreestyleDatabase.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace FreestyleDatabase.Shared
{
    partial class WrestlerMatches
    {
        [Parameter]
        public List<WrestlingDataModel> Matches { get; set; } = new List<WrestlingDataModel>();
    }
}