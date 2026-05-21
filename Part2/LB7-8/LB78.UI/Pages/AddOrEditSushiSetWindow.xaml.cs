namespace LB78.UI.Pages;

public partial class AddOrEditSushiSetWindow : ContentPage
{
    public AddOrEditSushiSetWindow(ViewModels.AddOrEditSushiSetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
