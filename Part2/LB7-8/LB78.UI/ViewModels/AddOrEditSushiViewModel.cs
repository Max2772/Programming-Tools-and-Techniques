using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LB78.Application.SushiSetUseCases.Queries;
using LB78.Application.SushiUseCases.Commands;
using LB78.UI.Services;
using System.Collections.ObjectModel;

namespace LB78.UI.ViewModels;

public partial class AddOrEditSushiViewModel(IMediator mediator) : ObservableObject, IQueryAttributable
{
    IAddOrEditSushiRequest _request = null!;
    private string? _pendingImagePath;

    [ObservableProperty] string name = string.Empty;
    [ObservableProperty] int readyCount;
    [ObservableProperty] decimal weight;
    [ObservableProperty] string description = string.Empty;
    [ObservableProperty] string photoPath = "dotnet_bot.png";

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
        PhotoPath = ResolvePhotoPath(_request.Sushi);

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

        if (!string.IsNullOrEmpty(_pendingImagePath) && _request.Sushi.Id > 0)
        {
            var savedPath = await ImageStorageService.SaveImageAsync(_request.Sushi.Id, _pendingImagePath);
            if (savedPath != null)
            {
                _request.Sushi.PhotoPath = savedPath;
                await mediator.Send(new EditSushiRequest() { Sushi = _request.Sushi });
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

    private static string ResolvePhotoPath(Sushi item)
    {
        var path = ImageStorageService.GetImagePath(item.Id);
        if (File.Exists(path))
            return path;
        return string.IsNullOrEmpty(item.PhotoPath) ? "dotnet_bot.png" : item.PhotoPath;
    }
}
