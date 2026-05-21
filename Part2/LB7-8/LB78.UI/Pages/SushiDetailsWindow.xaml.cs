namespace LB78.UI.Pages;

public partial class SushiDetailsWindow : ContentPage
{
    public SushiDetailsWindow(ViewModels.SushiDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
