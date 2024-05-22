using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace RazorSCLibrary.Components
{
    public abstract partial class CarouselById : ComponentBase, IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public string? ParentId { get; set; }

        [Parameter] public required string Id { get; set; }

        public RenderFragment[]? Items { get; set; }

        protected int currentSlide = 0;
        protected int totalNumberOfItems = 0;

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            base.OnInitialized();
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            OnLocationChanged(e.Location.Substring(NavigationManager.BaseUri.Length));
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
            NavigationManager.NavigateTo($"#{GetFullId(index)}", true, true);
        }

        private string GetFullId(int id) => $"{Id}-{id}";

        private void OnLocationChanged(string locationId)
        {
            if (locationId.Equals("#" + ParentId))
                OnCarouselFocused();
            else if (locationId.Contains("#" + Id))
                OnItemFocused(int.Parse(locationId.Substring(("#" + Id + "-").Length)));
        }

        protected virtual void OnCarouselFocused(){}

        protected virtual void OnItemFocused(int id)
        {
            currentSlide = id;
            StateHasChanged();            
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }
    }
}
