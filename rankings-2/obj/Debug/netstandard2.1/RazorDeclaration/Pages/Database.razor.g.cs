#pragma checksum "/Users/camstewart/Desktop/rankings-2/rankings-2/Pages/Database.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7e93da9aa1bed52a20c2fa34c1a0b3ef74482e8a"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace rankings_2.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using rankings_2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using rankings_2.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using rankings2.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "/Users/camstewart/Desktop/rankings-2/rankings-2/_Imports.razor"
using Radzen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/camstewart/Desktop/rankings-2/rankings-2/Pages/Database.razor"
using Radzen.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/Users/camstewart/Desktop/rankings-2/rankings-2/Pages/Database.razor"
using rankings2.Models;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Database")]
    public partial class Database : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 91 "/Users/camstewart/Desktop/rankings-2/rankings-2/Pages/Database.razor"
       
    public string text;
    public string country;
    public string emoji;
    public async Task OnChange()
    {
        wrestlingData = await Http.SearchWrestlerDataAsync(text, country, emoji);
        this.StateHasChanged();
    }

    private List<WrestlingDataModel> wrestlingData;

    protected override async Task OnInitializedAsync()
    {
        wrestlingData = await Http.GetWrestlerDataAsync();
    }



#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private WrestlingDataService Http { get; set; }
    }
}
#pragma warning restore 1591