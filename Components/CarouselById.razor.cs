using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace RazorSCLibrary.Components
{
    public abstract partial class CarouselById : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public string? ParentId { get; set; }

        [Parameter] public required string Id { get; set; }

        public RenderFragment[]? Items { get; set; }        

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
            base.OnInitialized();
        }

        private void NavigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            OnLocationChanged(e.Location.Substring(NavigationManager.BaseUri.Length));
        }        

        protected override async Task ShowItem(int index)
        {
            currentIndex = index;
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
            currentIndex = id;
            StateHasChanged();            
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
        }
    }
}
