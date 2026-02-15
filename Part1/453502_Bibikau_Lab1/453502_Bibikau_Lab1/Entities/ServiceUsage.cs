using System.Numerics;

namespace _453502_Bibikau_Lab1
{
    public class ServiceUsage<T> where T: INumber<T>
    {
        public User User { get; set; }
        public Tariff<T> Tariff { get; set; }
        public T Amount { get; set; }

        public ServiceUsage(User user, Tariff<T> tariff, T amount)
        {
            User = user;
            Tariff = tariff;
            Amount = amount;
        }
        
        public T CalculateCost() => Tariff.Price * Amount; 
    }
}