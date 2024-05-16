using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace RazorSCLibrary.Components
{
    public abstract partial class CarouselBase : ComponentBase, IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private IDisposable registration;
        [CascadingParameter]
        public string ParentId { get; set; }

        [Parameter] public required string Id { get; set; }

        public RenderFragment? Content { get; set; }
        public RenderFragment[]? Items { get; set; }

        protected int currentSlide = 0;
        protected int totalNumberOfItems = 0;

        protected override void OnInitialized()
        {                   
            Console.WriteLine(ParentId);
            base.OnInitialized();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                registration = NavigationManager.RegisterLocationChangingHandler(LocationChangingHandler);
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        private async ValueTask LocationChangingHandler(LocationChangingContext context)
        {
            string newUri = context.TargetLocation;
            Console.WriteLine(newUri);
            if (newUri.Equals("#" + ParentId))
                OnCarouselFocused();
            else if (newUri.Contains("#" + Id))
                OnItemFocused(int.Parse(newUri.Substring(("#" + Id + "-").Length)));
        }

        protected virtual void ScrollLeft()
        {
            int nextPosition = (currentSlide == 0) ? totalNumberOfItems - 1 : currentSlide - 1;            
            ScrollTo(nextPosition);
        }

        protected virtual void ScrollRight()
        {
            int nextPosition = (currentSlide == totalNumberOfItems - 1) ? 0 : currentSlide + 1;            
            ScrollTo(nextPosition);
        }

        protected virtual void ScrollTo(int index)
        {
            currentSlide = index;
            NavigationManager.NavigateTo($"#{GetFullId(index)}", false);
        }

        private string GetFullId(int id) => $"{Id}-{id}";

        protected virtual void OnCarouselFocused()
        {
            Console.WriteLine("Carousel Focused");
        }

        protected virtual void OnItemFocused(int id)
        {
            Console.WriteLine("Item" + id + " focused");
        }

        protected virtual void OnFinishedScrolling() { }

        public void Dispose()
        {
            registration?.Dispose();            
        }
    }
}
