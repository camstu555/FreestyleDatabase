using FreestyleDatabase.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FreestyleDatabase.Shared
{
    partial class WrestlerMatch
    {
        [Parameter]
        public WrestlingDataModel Match { get; set; } = new WrestlingDataModel();
    }
}