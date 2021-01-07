using Microsoft.AspNetCore.Components;

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