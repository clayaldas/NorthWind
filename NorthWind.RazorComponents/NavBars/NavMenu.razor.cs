//using Microsoft.AspNetCore.Components;

namespace NorthWind.RazorComponents.NavBars;

// Este componente permite renderizar una barra de navegación que tiene como secciones: 
// un encabezado (NavBarBrand) y los diferentes items del menu de navegación (NavBarItems)
public partial class NavMenu
{
    [Parameter]
    public RenderFragment NavBarBrand { get; set; }
    [Parameter]
    public RenderFragment NavBarItems { get; set; }

    bool CollapseNaveMenu = true;

    string NavMenuCssClass => CollapseNaveMenu ? "collapse": null;

    void ToggleNavMenu ()
    {
        CollapseNaveMenu = !CollapseNaveMenu;
    }
}
