using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared
{
    partial class MetalSection
    {
        [Parameter]
        public string Type { get; set; } = "Gold";

        [Parameter]
        public string Name { get; set; } = "Tournament";
    }
}
