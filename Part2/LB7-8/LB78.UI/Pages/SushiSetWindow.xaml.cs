namespace LB78.UI.Pages;

public partial class SushiSetWindow : ContentPage
{
    public SushiSetWindow(ViewModels.SushiSetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
