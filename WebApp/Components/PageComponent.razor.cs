using Microsoft.AspNetCore.Components;

namespace WebApp.Components;

public partial class PageComponent
{
    [Parameter]
    public string Title { get; set; } = "Naslov";

    [Parameter]
    public RenderFragment ChildContent { get; set; }
}
