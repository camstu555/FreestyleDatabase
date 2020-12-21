using FreestyleDatabase.Shared.Models;
using FreestyleDatabase.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreestyleDatabase.Pages
{
    partial class WrestlerProfile
    {
        public WrestlingAutoCompleteModel selectedWrestler;

        public WrestlingAggregatesModel result;

        public string SubTitle
        {
            get
            {
                if (result == null)
                {
                    return string.Empty;
                }

                return $"{result.WrestlerWeight} KG <em style='color: var(--secondary)'>|</em> {result.WrestlerCountry}";
            }
        }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        private WrestlerSearchService Http { get; set; }

        [Inject]
        private NavigationManager Nav { get; set; }

        public void Dispose()
        {
            Nav.LocationChanged -= LocationChanged;
        }

        protected override async Task OnInitializedAsync()
        {
            result = await Http.GetWrestlerDetails(Id);
            Nav.LocationChanged += LocationChanged;
        }

        private async Task SelectedResultChanged(WrestlingAutoCompleteModel result)
        {
            var wrestler = await Http.GetWrestlerDetailsByName(result.Text);

            Nav.NavigateTo("WrestlerProfile/" + wrestler.WrestlerId);
        }

        private void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            InvokeAsync(async () =>
            {
                await Task.Delay(1);
                result = await Http.GetWrestlerDetails(Id);
                StateHasChanged();
            });
        }

        private async Task<IEnumerable<WrestlingAutoCompleteModel>> SearchWrestlers(string searchText)
        {
            var response = await Http.GetAutoComplete(searchText);
            return response.Items;
        }
    }
}