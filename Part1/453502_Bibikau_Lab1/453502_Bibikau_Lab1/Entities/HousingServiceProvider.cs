using System.Numerics;

namespace _453502_Bibikau_Lab1
{
    public class HousingServiceProvider<T> : IHousingServices<T> where T: INumber<T>
    {
        private MyCustomCollection<Tariff<T>> Tariffs = new();
        private MyCustomCollection<User> Users = new();
        private MyCustomCollection<ServiceUsage<T>> ServiceUsages = new();
        
        public void AddTariff(Tariff<T> tariff)
        {
            Tariffs.Add(tariff);
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void AddServiceUsage(string userName, string tariffName, T amount)
        {
            var user = FindUserByName(userName);
            var tariff = FindTariffByName(tariffName);
            
            if (user != null && tariff != null)
            {
                ServiceUsages.Add(new ServiceUsage<T>(user, tariff, amount));
            }
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