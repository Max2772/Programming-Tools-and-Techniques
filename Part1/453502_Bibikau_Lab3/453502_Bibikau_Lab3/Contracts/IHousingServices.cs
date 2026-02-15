namespace _453502_Bibikau_Lab3
{
    public interface IHousingServices
    {
        void AddTariff(Tariff tariff);
        void AddUser(User user);
        void AddServiceUsage(string userName, string tariffName, decimal amount);

        List<string> GetTariffNamesSortedByCost();
        decimal GetTotalCost();
        decimal GetTotalCostForUser(string userName);
        string? GetUsernameWithMaxPayment();
        int GetUserCountWithCostGreaterThan(decimal amount);
        void GetUserPaymentsForTariffs();
    }
}