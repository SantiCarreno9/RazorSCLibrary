using Microsoft.AspNetCore.Components;

namespace RazorSCLibrary.Components
{
    public abstract class CarouselByScript : CarouselBase
    {
        [Inject]
        JsMethods? JsMethods { get; set; }

        [Parameter]
        public bool SmoothMovement { get; set; } = true;
        

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }       

        protected override async Task ShowItem(int position)
        {            
            int delta = position - currentIndex;
            currentIndex = position;
            if(JsMethods!=null)
            {
                bool finished = await JsMethods.ScrollLeft(contentContainerReference, delta,SmoothMovement);
                if (finished)
                    OnItemFocused();
            }
            await base.ShowItem(position);
        }        
    }
}
