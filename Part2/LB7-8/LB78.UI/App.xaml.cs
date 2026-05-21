namespace LB78.UI;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Routing.RegisterRoute(nameof(Pages.SushiDetailsWindow), typeof(Pages.SushiDetailsWindow));
        Routing.RegisterRoute(nameof(Pages.AddOrEditSushiSetWindow), typeof(Pages.AddOrEditSushiSetWindow));
        Routing.RegisterRoute(nameof(Pages.AddOrEditSushiWindow), typeof(Pages.AddOrEditSushiWindow));
    }
}
