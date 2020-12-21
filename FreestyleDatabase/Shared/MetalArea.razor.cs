using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared
{
    partial class MetalArea
    {
        [Inject]
        private IJSRuntime Js { get; set; }

        /// <summary>
        /// returns WrestlerName1 and Round = Gold
        /// </summary>
        [Parameter]
        public List<string> GoldMedalMatches { get; set; } = new List<string>();

        /// <summary>
        /// returns WrestlerName2 and Round = Gold
        /// </summary>
        [Parameter]
        public List<string> SilverMedalMatches { get; set; } = new List<string>();

        /// <summary>
        /// returns WrestlerName1 and Round = Bronze
        /// </summary>
        [Parameter]
        public List<string> BronzeMedalMatches { get; set; } = new List<string>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await Js.InvokeVoidAsync("window.yourJsInterop.staggerMetals");
        }

    }
}
