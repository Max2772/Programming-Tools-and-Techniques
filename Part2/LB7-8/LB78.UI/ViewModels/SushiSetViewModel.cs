using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LB78.Application.SushiSetUseCases.Commands;
using LB78.Application.SushiSetUseCases.Queries;
using LB78.Application.SushiUseCases.Commands;
using LB78.Application.SushiUseCases.Queries;
using LB78.UI.Pages;
using LB78.UI.Services;
using System.Collections.ObjectModel;

namespace LB78.UI.ViewModels;

public partial class SushiSetViewModel(IMediator mediator) : ObservableObject
{
    private readonly IMediator _mediator = mediator;

    public ObservableCollection<SushiSet> SushiSets { get; set; } = new();
    public ObservableCollection<Sushi> SushiItems { get; set; } = new();

    [ObservableProperty]
    SushiSet? selectedSushiSet;

    [RelayCommand]
    public async Task UpdateGroupList() => await GetSushiSets();

    [RelayCommand]
    public async Task UpdateMembersList() => await GetSushi();

    [RelayCommand]
    public async Task ShowDetails(Sushi sushi)
    {
        await GotoDetailsPage(sushi);
        await GetSushi();
    }

    [RelayCommand]
    public async Task AddSushiSet()
    {
        await GoToAddOrEditPage(nameof(AddOrEditSushiSetWindow), new AddSushiSetRequest() { SushiSet = new SushiSet() });
        await GetSushiSets();
    }

    [RelayCommand]
    public async Task EditSushiSet()
    {
        if (SelectedSushiSet is null)
            return;
        await GoToAddOrEditPage(nameof(AddOrEditSushiSetWindow),
            new EditSushiSetRequest() { SushiSet = SelectedSushiSet });
        await GetSushiSets();
    }

    [RelayCommand]
    public async Task DeleteSushiSet()
    {
        if (SelectedSushiSet is null)
            return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Удаление",
            $"Удалить набор «{SelectedSushiSet.Name}» и все суши в нём?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        await _mediator.Send(new DeleteSushiSetRequest(SelectedSushiSet));
        SelectedSushiSet = null;
        await GetSushiSets();
        await GetSushi();
    }

    [RelayCommand]
    public async Task AddSushi()
    {
        if (SelectedSushiSet is null)
            return;
        await GoToAddOrEditPage(nameof(AddOrEditSushiWindow),
            new AddSushiRequest() { Sushi = new Sushi() { SushiSetId = SelectedSushiSet.Id } });
        await GetSushi();
    }

    private async Task GetSushiSets()
    {
        var sushiSets = await _mediator.Send(new GetAllSushiSetRequest());
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            SushiSets.Clear();
            foreach (var sushiSet in sushiSets)
                SushiSets.Add(sushiSet);
        });
    }

    private async Task GetSushi()
    {
        if (SelectedSushiSet == null) return;

        var items = await _mediator.Send(new GetBySushiSetSushiRequest(SelectedSushiSet.Id));
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            SushiItems.Clear();
            foreach (var item in items)
            {
                var path = ImageStorageService.GetImagePath(item.Id);
                if (File.Exists(path))
                    item.PhotoPath = path;
                else if (string.IsNullOrEmpty(item.PhotoPath))
                    item.PhotoPath = "dotnet_bot.png";
                SushiItems.Add(item);
            }
        });
    }

    private async Task GotoDetailsPage(Sushi sushi)
    {
        IDictionary<string, object> parameters =
            new Dictionary<string, object>()
            {
                { "Sushi", sushi }
            };
        await Shell.Current.GoToAsync(nameof(SushiDetailsWindow), parameters);
    }

    private async Task GoToAddOrEditPage(string route, IRequest request)
    {
        await Shell.Current.GoToAsync(route, new Dictionary<string, object>()
        {
            { "Request", request }
        });
    }
}
