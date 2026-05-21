namespace LB78.UI.Pages;

public partial class AddOrEditSushiWindow : ContentPage
{
    public AddOrEditSushiWindow(ViewModels.AddOrEditSushiViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
