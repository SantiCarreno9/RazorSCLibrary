using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RazorSCLibrary.Components
{
    public abstract partial class CarouselBase : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        JsMethods JsMethods { get; set; }

        public RenderFragment[] Items { get; set; }

        private ElementReference scrollerReference;

        protected int currentPosition = 0;
        protected int totalNumberOfItems = 5;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            JsMethods = new(JSRuntime);
        }

        protected async Task ScrollLeft()
        {
            int nextPosition = (currentPosition == 0) ? totalNumberOfItems - 1 : currentPosition - 1;
            await ScrollTo(nextPosition);
        }

        protected async Task ScrollRight()
        {
            int nextPosition = (currentPosition == totalNumberOfItems - 1) ? 0 : currentPosition + 1;
            await ScrollTo(nextPosition);
        }

        protected virtual async Task ScrollTo(int position)
        {
            int delta = position - currentPosition;
            currentPosition = position;
            bool finished = await JsMethods.ScrollLeft(scrollerReference, delta);
            if (finished)
                OnFinishedScrolling();
        }

        protected virtual void OnFinishedScrolling() { }
    }
}
