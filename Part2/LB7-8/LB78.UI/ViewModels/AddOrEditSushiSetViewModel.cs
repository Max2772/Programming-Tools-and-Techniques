using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LB78.Application.SushiSetUseCases.Commands;
using LB78.UI.Services;

namespace LB78.UI.ViewModels;

public partial class AddOrEditSushiSetViewModel(IMediator mediator) : ObservableObject, IQueryAttributable
{
    IAddOrEditSushiSetRequest _request = null!;
    private string? _pendingImagePath;

    [ObservableProperty] string name = string.Empty;
    [ObservableProperty] decimal price = -1;
    [ObservableProperty] string photoPath = "dotnet_bot.png";

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _request = (query["Request"] as IAddOrEditSushiSetRequest)!;
        Name = _request.SushiSet.Name;
        Price = _request.SushiSet.Price;
        PhotoPath = ResolvePhotoPath(_request.SushiSet);
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

        if (!string.IsNullOrEmpty(_pendingImagePath) && _request.SushiSet.Id > 0)
        {
            var savedPath = await ImageStorageService.SaveImageAsync(_request.SushiSet.Id, _pendingImagePath);
            if (savedPath != null)
            {
                _request.SushiSet.PhotoPath = savedPath;
                await mediator.Send(new EditSushiSetRequest() { SushiSet = _request.SushiSet });
            }
        }

        await GoBack();
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task SelectImage()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Выберите изображение"
        });

        if (result != null)
        {
            string targetFolder = Path.Combine(FileSystem.AppDataDirectory, "Images");
            Directory.CreateDirectory(targetFolder);
            string tempFile = Path.Combine(targetFolder, $"temp_{Guid.NewGuid()}.jpg");

            await using (var sourceStream = await result.OpenReadAsync())
            await using (var destinationStream = File.Create(tempFile))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }

            _pendingImagePath = tempFile;
            PhotoPath = tempFile;
        }
    }

    private static string ResolvePhotoPath(SushiSet item)
    {
        var path = ImageStorageService.GetImagePath(item.Id);
        if (File.Exists(path))
            return path;
        return string.IsNullOrEmpty(item.PhotoPath) ? "dotnet_bot.png" : item.PhotoPath;
    }
}
