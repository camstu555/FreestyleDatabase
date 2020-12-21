using Microsoft.AspNetCore.Components;

namespace FreestyleDatabase.Shared
{
    partial class TitleSection
    {
        [Parameter]
        public string Title { get; set; } = "Page Name";

        [Parameter]
        public string SubTitle { get; set; } = "Your Page Sub Title";

        [Parameter]
        public bool NoMargins { get; set; } = false;

        private MarkupString Raw(string val)
        {
            return new MarkupString(val ?? string.Empty);
        }
    }
}