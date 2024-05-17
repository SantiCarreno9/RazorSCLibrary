using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RazorSCLibrary
{
    public class JsMethods : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public JsMethods(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/RazorSCLibrary/js/misc.js").AsTask());
        }

        public async ValueTask<bool> IsElementVisible(ElementReference element)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("isElementVisible", element);
        }

        public async ValueTask<bool> ScrollLeft(ElementReference element, int amount)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("scrollLeft", element, amount);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
