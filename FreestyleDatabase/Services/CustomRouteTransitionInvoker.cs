using BlazorTransitionableRoute;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreestyleDatabase.Services
{
    public class CustomRouteTransitionInvoker : IRouteTransitionInvoker
    {
        private readonly IJSRuntime jsRuntime;

        public CustomRouteTransitionInvoker(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task InvokeRouteTransitionAsync(bool backwards)
            => await jsRuntime.InvokeVoidAsync("window.yourJsInterop.transitionFunction", backwards);
    }
}
