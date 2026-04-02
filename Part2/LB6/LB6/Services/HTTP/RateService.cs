using LB6.Entities.Http;
using Newtonsoft.Json;

namespace LB6.Services.Http;

public class RateService : IRateService
{
    HttpClient _httpClient;

    public RateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Rate>> GetRates(DateTime date)
    {
        var formattedDate = date.ToString("yyyy-MM-dd");
        var response = await _httpClient.GetAsync($"exrates/rates?ondate={formattedDate}&periodicity=0");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var rates = JsonConvert.DeserializeObject<IEnumerable<Rate>>(content) ?? Enumerable.Empty<Rate>();;
            return rates;
        }
        else
        {
            return Enumerable.Empty<Rate>();
        }

    }
}