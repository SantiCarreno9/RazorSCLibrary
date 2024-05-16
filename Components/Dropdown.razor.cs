using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace RazorSCLibrary.Components
{
    public partial class Dropdown
    {
        //[Parameter] public required RenderFragment ChildContent { get; set; }

        [Parameter] public required RenderFragment Title { get; set; }

        [Parameter] public required int Id { get; set; }

        [Parameter] public string Text { get; set; } = string.Empty;

        [Parameter] public Action<int> OnClicked { get; set; }

        [Parameter] public List<RenderFragment>? DropdownItems { get; set; }

        [Parameter]
        public bool State
        {
            get { return isSelected; }
            set { if (isSelected != value) isSelected = value; }
        }

        private bool isSelected = false;

        private void TriggerAction()
        {
            isSelected = true;
            if (OnClicked != null) OnClicked(Id);
        }
    }
}
