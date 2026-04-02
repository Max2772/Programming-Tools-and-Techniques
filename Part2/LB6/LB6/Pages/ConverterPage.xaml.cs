using System.Collections.ObjectModel;
using LB6.Entities.Http;
using LB6.Services.Http;

namespace LB6.Pages;

public partial class ConverterPage : ContentPage
{
    private readonly IRateService _rateService;
    private readonly ObservableCollection<Rate> _rates = new();
    private readonly ObservableCollection<Currency> _currencies = new();
    private bool _isToByn = true;

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

    private void OnDirectionToggled(object sender, EventArgs e)
    {
        _isToByn = !_isToByn;
        UpdateDirectionUI();
        HideMessages();
    }

    private void UpdateDirectionUI()
    {
        if (_isToByn)
        {
            DirectionLabel.Text = "Валюта  →  BYN";
            EntryHintLabel.Text = "Введите сумму в иностранной валюте";
        }
        else
        {
            DirectionLabel.Text = "BYN  →  Валюта";
            EntryHintLabel.Text = "Введите сумму в белорусских рублях";
        }
    }

    private async void OnConvertClicked(object sender, EventArgs e)
    {
        HideMessages();

        if (CurrencyPicker.SelectedItem is not Currency selectedCurrency)
        {
            ShowError("Пожалуйста, выберите валюту.");
            return;
        }

        if (!TryParseAmount(ValueEntry.Text, out decimal amount))
        {
            ShowError("Пожалуйста, введите корректное положительное число.");
            return;
        }

        try
        {
            var ratesForDate = await _rateService.GetRates(DatePicker.Date);
            var rate = ratesForDate.FirstOrDefault(r =>
                r.Cur_Abbreviation == selectedCurrency.Cur_Abbreviation);

            if (rate is null)
            {
                ShowError($"Курс для {selectedCurrency.Cur_Abbreviation} на {DatePicker.Date:dd MMMM yyyy} не найден.");
                return;
            }

            ShowConversionResult(amount, rate);
        }
        catch (Exception ex)
        {
            ShowError($"Не удалось получить курс: {ex.Message}");
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
            {
                if (_supportedCurrencies.Any(c => c.Cur_Abbreviation == rate.Cur_Abbreviation))
                    _rates.Add(rate);
            }

            LastUpdatedLabel.Text = $"Обновлено: {date:dd MMMM yyyy}";
        }
        catch (Exception ex)
        {
            ShowError($"Не удалось загрузить курсы: {ex.Message}");
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

    private static decimal CalculateConversion(decimal amount, Rate rate, bool toByn)
    {
        decimal ratePerUnit = (decimal)rate.Cur_OfficialRate / rate.Cur_Scale;
        return toByn
            ? amount * ratePerUnit   // иностранная → BYN
            : amount / ratePerUnit;  // BYN → иностранная
    }

    private void ShowConversionResult(decimal amount, Rate rate)
    {
        decimal result = CalculateConversion(amount, rate, _isToByn);

        ResultLabel.Text = _isToByn
            ? $"{amount} {rate.Cur_Abbreviation} = {result:F4} BYN"
            : $"{amount} BYN = {result:F4} {rate.Cur_Abbreviation}";

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