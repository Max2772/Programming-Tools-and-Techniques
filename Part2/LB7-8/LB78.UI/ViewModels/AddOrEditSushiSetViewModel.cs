using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LB78.Application.SushiSetUseCases.Commands;

namespace LB78.UI.ViewModels;

public partial class AddOrEditSushiSetViewModel(IMediator mediator) : ObservableObject, IQueryAttributable
{
    IAddOrEditSushiSetRequest _request = null!;

    [ObservableProperty] string name = string.Empty;
    [ObservableProperty] decimal price = -1;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _request = (query["Request"] as IAddOrEditSushiSetRequest)!;
        Name = _request.SushiSet.Name;
        Price = _request.SushiSet.Price;
    }

    [RelayCommand]
    public async Task AddOrEditSushiSet()
    {
        if (string.IsNullOrEmpty(Name))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Название не может быть пустым.", "OK");
            return;
        }
        if (Price < 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Цена не может быть отрицательной.", "OK");
            return;
        }

        _request.SushiSet.Name = Name;
        _request.SushiSet.Price = Price;
        await mediator.Send(_request);
        await GoBack();
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
