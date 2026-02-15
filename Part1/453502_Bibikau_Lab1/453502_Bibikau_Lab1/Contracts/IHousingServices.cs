using System.Numerics;

namespace _453502_Bibikau_Lab1
{
    public interface IHousingServices<T> where T: INumber<T>
    {
        void AddTariff(Tariff<T> tariff);
        void AddUser(User user);
        void AddServiceUsage(string userName, string tariffName, T amount);
        MyCustomCollection<ServiceUsage<T>> GetServicesForUser(string userName);
        T TotalServicesForTariff(string tariffName);
        T CalculateTotalCost();
    }
}