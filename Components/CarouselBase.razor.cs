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

        public RenderFragment[]? Items { get; set; }

        protected int currentSlide = 0;
        protected int totalNumberOfItems = 0;

        protected override void OnInitialized()
        {
            Console.WriteLine(ParentId);
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            base.OnInitialized();
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            Console.WriteLine(e.Location);
            OnLocationChanged(e.Location.Substring(NavigationManager.BaseUri.Length));
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //registration = NavigationManager.RegisterLocationChangingHandler(LocationChangingHandler);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async ValueTask LocationChangingHandler(LocationChangingContext context)
        {
            string newUri = context.TargetLocation;
            Console.WriteLine(newUri);
            OnLocationChanged(newUri);

            await ValueTask.CompletedTask;
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
            NavigationManager.NavigateTo($"#{GetFullId(index)}",true);
        }

        private string GetFullId(int id) => $"{Id}-{id}";

        private void OnLocationChanged(string locationId)
        {
            if (locationId.Equals("#" + ParentId))
                OnCarouselFocused();
            else if (locationId.Contains("#" + Id))
                OnItemFocused(int.Parse(locationId.Substring(("#" + Id + "-").Length)));
        }

        protected virtual void OnCarouselFocused()
        {
            Console.WriteLine("Carousel Focused");
        }

        protected virtual void OnItemFocused(int id)
        {
            currentSlide = id;
            Console.WriteLine("Item" + id + " focused");
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
            registration?.Dispose();
        }
    }
}
