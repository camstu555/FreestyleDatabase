using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared
{
    partial class ByTheNumbers
    {
        [Parameter]
        public string Image { get; set; } = "image";

        [Parameter]
        public int Wins { get; set; } = 0;

        [Parameter]
        public int Losses { get; set; } = 0;

        [Parameter]
        public DateTimeOffset? Since { get; set; } = new DateTimeOffset?();

        [Parameter]
        public int Pins { get; set; } = 0;

        [Parameter]
        public int Techs { get; set; } = 0;

        [Parameter]
        public int Points { get; set; } = 0;

        [Parameter]
        public double Bonus { get; set; } = 0.0;

        [Parameter]
        public double OffPts { get; set; } = 0.0;

        [Parameter]
        public double DefPts { get; set; } = 0.0;

    }
}
