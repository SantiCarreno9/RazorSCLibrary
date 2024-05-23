using Microsoft.AspNetCore.Components;

namespace RazorSCLibrary.Components
{
    public abstract partial class CarouselBase : ComponentBase
    {
        [Parameter]
        public bool ShowIndicators { get; set; } = true;
        [Parameter]
        public RenderFragment? DummyItem { get; set; }

        public IEnumerable<RenderFragment>? Items { get; set; }

        protected ElementReference contentContainerReference { get; private set; }
        protected int currentIndex = 0;
        protected int totalNumberOfItems = 0;

        protected virtual async Task ShowPreviousItem()
        {
            int previousIndex = (currentIndex == 0) ? totalNumberOfItems - 1 : currentIndex - 1;
            await ShowItem(previousIndex);
        }

        protected virtual async Task ShowNextItem()
        {
            int nextIndex = (currentIndex == totalNumberOfItems - 1) ? 0 : currentIndex + 1;
            await ShowItem(nextIndex);
        }

        protected virtual async Task ShowItem(int index)
        {            
            currentIndex = index;
        }

        protected virtual void OnItemFocused() { }        
    }
}
