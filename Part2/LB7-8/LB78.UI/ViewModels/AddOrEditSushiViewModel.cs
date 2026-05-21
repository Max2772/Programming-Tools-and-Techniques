using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LB78.Application.SushiSetUseCases.Queries;
using LB78.Application.SushiUseCases.Commands;
using System.Collections.ObjectModel;

namespace LB78.UI.ViewModels;

public partial class AddOrEditSushiViewModel(IMediator mediator) : ObservableObject, IQueryAttributable
{
    IAddOrEditSushiRequest _request = null!;

    [ObservableProperty] string name = string.Empty;
    [ObservableProperty] int readyCount;
    [ObservableProperty] decimal weight;
    [ObservableProperty] string description = string.Empty;

    public ObservableCollection<SushiSet> SushiSets { get; set; } = new();
    [ObservableProperty] SushiSet? selectedSushiSet;
    [ObservableProperty] bool isSushiSetIdChangeable = true;
    [ObservableProperty] string pickerTitle = "Выберите набор суши";

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _request = (query["Request"] as IAddOrEditSushiRequest)!;
        Name = _request.Sushi.Name;
        ReadyCount = _request.Sushi.ReadyCount;
        Weight = _request.Sushi.Weight;
        Description = _request.Sushi.Description;

        if (_request.Sushi.SushiSetId != 0 && string.IsNullOrEmpty(_request.Sushi.Name))
        {
            SelectedSushiSet = await mediator.Send(new GetByIdSushiSetRequest(_request.Sushi.SushiSetId));
            IsSushiSetIdChangeable = false;
            PickerTitle = "Набор выбран";
        }
        else if (_request.Sushi.SushiSetId != 0)
        {
            PickerTitle = $"Изначально выбран набор с Id {_request.Sushi.SushiSetId}";
        }

        await GetSushiSets();
    }

    [RelayCommand]
    public async Task GetSushiSets()
    {
        var sushiSets = await mediator.Send(new GetAllSushiSetRequest());
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            SushiSets.Clear();
            foreach (var sushiSet in sushiSets)
                SushiSets.Add(sushiSet);
        });
    }

    [RelayCommand]
    public async Task AddOrEditSushi()
    {
        if (string.IsNullOrEmpty(Name))
        {
            await Shell.Current.DisplayAlert("Ошибка", "Название не может быть пустым", "OK");
            return;
        }
        if (ReadyCount < 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Количество готовых суши не может быть отрицательным", "OK");
            return;
        }

        _request.Sushi.Name = Name;
        _request.Sushi.ReadyCount = ReadyCount;
        _request.Sushi.Weight = Weight;
        _request.Sushi.Description = Description;
        _request.Sushi.SushiSetId = SelectedSushiSet?.Id ?? _request.Sushi.SushiSetId;

        await mediator.Send(_request);
        await GoBack();
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
