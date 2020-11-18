using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace FreestyleDatabase.Services
{
    public class QueryParameterService
    {
        private readonly NavigationManager navigationManager;

        public QueryParameterService(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        public string this[string key]
        {
            get
            {
                var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var schmeckle))
                {
                    return schmeckle.ToString();
                }

                return string.Empty;
            }
        }
    }
}