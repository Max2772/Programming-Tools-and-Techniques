using LB6.Entities.Http;

namespace LB6.Services.Http;

public interface IRateService
{
    Task<IEnumerable<Rate>> GetRates(DateTime date);
}