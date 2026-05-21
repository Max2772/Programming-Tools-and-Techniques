using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LB78.Application.SushiUseCases.Commands;
using LB78.Application.SushiUseCases.Queries;
using LB78.UI.Pages;
using LB78.UI.Services;

namespace LB78.UI.ViewModels;

[QueryProperty("Sushi", "Sushi")]
public partial class SushiDetailsViewModel(IMediator mediator) : ObservableObject
{
    [ObservableProperty] private Sushi sushi = null!;

    [ObservableProperty] string name = string.Empty;
    [ObservableProperty] int readyCount;
    [ObservableProperty] decimal weight;
    [ObservableProperty] string description = string.Empty;
    [ObservableProperty] string photoPath = string.Empty;

    [RelayCommand]
    async Task GetByIdSushi()
    {
        Sushi = (await mediator.Send(new GetByIdSushiRequest(Sushi.Id)))!;
        Name = Sushi.Name;
        ReadyCount = Sushi.ReadyCount;
        Weight = Sushi.Weight;
        Description = Sushi.Description;
        PhotoPath = ResolvePhotoPath(Sushi);
    }

    [RelayCommand]
    async Task EditSushi()
    {
        await Shell.Current.GoToAsync(nameof(AddOrEditSushiWindow), new Dictionary<string, object>()
        {
            { "Request", new EditSushiRequest() { Sushi = Sushi } }
        });
    }

    [RelayCommand]
    async Task DeleteSushi()
    {
        bool confirm = await Shell.Current.DisplayAlert(
            "Удаление",
            $"Удалить суши «{Sushi.Name}»?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        await mediator.Send(new DeleteSushiRequest(Sushi));

        var imagePath = ImageStorageService.GetImagePath(Sushi.Id);
        if (File.Exists(imagePath))
            File.Delete(imagePath);

        await Shell.Current.GoToAsync("..");
    }

    private static string ResolvePhotoPath(Sushi item)
    {
        var path = ImageStorageService.GetImagePath(item.Id);
        if (File.Exists(path))
            return path;
        return string.IsNullOrEmpty(item.PhotoPath) ? "dotnet_bot.png" : item.PhotoPath;
    }
}
