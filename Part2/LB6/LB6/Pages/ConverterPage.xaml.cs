using System.Collections.ObjectModel;
using LB6.Entities.Http;
using LB6.Services.Http;

namespace LB6.Pages;

public partial class ConverterPage : ContentPage
{

    private readonly IRateService _rateService;
    private readonly ObservableCollection<Rate> _rates = new();
    private readonly ObservableCollection<Currency> _currencies = new();

    private static readonly IReadOnlyList<Currency> _supportedCurrencies = new[]
    {
        new Currency { Cur_Abbreviation = "RUB", Cur_Name = "Российский рубль" },
        new Currency { Cur_Abbreviation = "EUR", Cur_Name = "Евро" },
        new Currency { Cur_Abbreviation = "USD", Cur_Name = "Доллар США" },
        new Currency { Cur_Abbreviation = "CHF", Cur_Name = "Швейцарский франк" },
        new Currency { Cur_Abbreviation = "CNY", Cur_Name = "Китайский юань" },
        new Currency { Cur_Abbreviation = "GBP", Cur_Name = "Фунт стерлингов" },
    };


    public ConverterPage(IRateService rateService)
    {
        InitializeComponent();

        _rateService = rateService;

        RatesCollectionView.ItemsSource = _rates;
        CurrencyPicker.ItemsSource = _currencies;

        DatePicker.Date = DateTime.Today;
        DatePicker.MaximumDate = DateTime.Today;

        _ = InitializeAsync();
    }


    private async Task InitializeAsync()
    {
        foreach (var currency in _supportedCurrencies)
            _currencies.Add(currency);

        await LoadRatesAsync(DatePicker.Date);
    }


    private async void OnDateSelected(object sender, DateChangedEventArgs e)
        => await LoadRatesAsync(e.NewDate);

    private async void OnConvertClicked(object sender, EventArgs e)
    {
        HideMessages();

        if (CurrencyPicker.SelectedItem is not Currency selectedCurrency)
        {
            ShowError("Please select a currency.");
            return;
        }

        if (!TryParseAmount(ValueEntry.Text, out decimal amount))
        {
            ShowError("Please enter a valid positive number.");
            return;
        }

        try
        {
            var ratesForDate = await _rateService.GetRates(DatePicker.Date);
            var rate = ratesForDate.FirstOrDefault(r =>
                r.Cur_Abbreviation == selectedCurrency.Cur_Abbreviation);

            if (rate is null)
            {
                ShowError($"No rate found for {selectedCurrency.Cur_Abbreviation} on {DatePicker.Date:dd MMMM yyyy}.");
                return;
            }

            ShowConversionResult(amount, rate);
        }
        catch (Exception ex)
        {
            ShowError($"Could not fetch rates: {ex.Message}");
        }
    }

    private async Task LoadRatesAsync(DateTime date)
    {
        SetLoading(true);
        HideMessages();

        try
        {
            var ratesForDate = await _rateService.GetRates(date);

            _rates.Clear();
            foreach (var rate in ratesForDate)
                _rates.Add(rate);

            LastUpdatedLabel.Text = $"Updated: {date:dd MMMM yyyy}";
        }
        catch (Exception ex)
        {
            ShowError($"Could not load rates: {ex.Message}");
        }
        finally
        {
            SetLoading(false);
        }
    }

    private static bool TryParseAmount(string? text, out decimal amount)
    {
        amount = 0;
        return !string.IsNullOrWhiteSpace(text)
               && decimal.TryParse(text, out amount)
               && amount > 0;
    }

    private static decimal CalculateConversion(decimal amount, Rate rate)
        => amount * (decimal)rate.Cur_OfficialRate / rate.Cur_Scale;

    private void ShowConversionResult(decimal amount, Rate rate)
    {
        decimal result = CalculateConversion(amount, rate);

        ResultLabel.Text = $"{amount} {rate.Cur_Abbreviation} = {result:F4} BYN";
        ResultCard.IsVisible = true;
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorCard.IsVisible = true;
    }

    private void HideMessages()
    {
        ResultCard.IsVisible = false;
        ErrorCard.IsVisible = false;
    }

    private void SetLoading(bool isLoading)
    {
        LoadingIndicator.IsRunning = isLoading;
        LoadingIndicator.IsVisible = isLoading;
    }
}