using System.Numerics;
using System.Reflection.Metadata;

namespace _453502_Bibikau_Lab2
{
    public class HousingServiceProvider<T> : IHousingServices<T> where T: INumber<T>
    {
        private MyCustomCollection<Tariff<T>> Tariffs = new();
        private MyCustomCollection<User> Users = new();
        private MyCustomCollection<ServiceUsage<T>> ServiceUsages = new();
        
        public event Action<Tariff<T>> TariffAdded;
        public event Action<Tariff<T>> TariffRemoved;
        public event Action<User> UserAdded;
        public event Action<User> UserRemoved;
        public event Action<ServiceUsage<T>> ChangeServiceUsagesCollection;
        
        public void AddTariff(Tariff<T> tariff)
        {
            Tariffs.Add(tariff);
            TariffAdded(tariff);
        }
        
        public void RemoveTariff(Tariff<T> tariff)
        {
            Tariffs.Remove(tariff);
            TariffRemoved(tariff);
        }
        

        public void AddUser(User user)
        {
            Users.Add(user);
            UserAdded(user);
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
            UserRemoved(user);
        }

        public void AddServiceUsage(string userName, string tariffName, T amount)
        {
            var user = FindUserByName(userName);
            var tariff = FindTariffByName(tariffName);
            
            if (user != null && tariff != null)
            {
                var serviceUsage = new ServiceUsage<T>(user, tariff, amount);
                ServiceUsages.Add(serviceUsage);
                ChangeServiceUsagesCollection(serviceUsage);
            }
            
            if (user == null)
                throw new ArgumentException($"User '{userName}' not found");

            if (tariff == null)
                throw new ArgumentException($"Tariff '{tariffName}' not found");

        }

        public MyCustomCollection<ServiceUsage<T>> GetServicesForUser(string userName)
        {
            MyCustomCollection<ServiceUsage<T>> userServices = new();
            
            for (int i = 0; i < ServiceUsages.Count; i++)
            {
                if (ServiceUsages[i].User.Name == userName)
                {
                    userServices.Add(ServiceUsages[i]);
                }
            }

            return userServices;
        }

        public T TotalServicesForTariff(string tariffName)
        {
            T total = T.Zero;
            for (int i = 0; i < ServiceUsages.Count; i++){
                if (ServiceUsages[i].Tariff.Name == tariffName)
                {
                    total++;
                }
            }

            return total;
        }

        public T CalculateTotalCost()
        {
            T totalCost = T.Zero;

            for (int i = 0; i < ServiceUsages.Count; ++i)
            {
                totalCost += ServiceUsages[i].CalculateCost();
            }

            return totalCost;
        }

        private User FindUserByName(string name)
        {
            Users.Reset();
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Name == name) return Users[i];
            }

            return null;
        }
        private Tariff<T> FindTariffByName(string name)
        {
            Tariffs.Reset();
            for (int i = 0; i < Tariffs.Count; i++)
            {
                if (Tariffs[i].Name == name) return Tariffs[i];
            }

            return null;
        }

    }
}