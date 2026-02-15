namespace _453502_Bibikau_Lab3
{
    public class HousingServiceProvider : IHousingServices
    {
        private Dictionary<string, decimal> Tariffs = new();
        private List<User> Users = new();
        private List<ServiceUsage> ServiceUsages = new();
        
        public event Action<Tariff> TariffAdded;
        public event Action<Tariff> TariffRemoved;
        public event Action<User> UserAdded;
        public event Action<User> UserRemoved;
        public event Action<ServiceUsage> ChangeServiceUsagesCollection;
        
        public void AddTariff(Tariff tariff)
        {
            Tariffs[tariff.Name] = tariff.Price;
            TariffAdded(tariff);
        }
        
        public void RemoveTariff(Tariff tariff)
        {
            Tariffs.Remove(tariff.Name);
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
        
        public void AddServiceUsage(string userName, string tariffName, decimal amount)
        {
            var user = Users.
                FirstOrDefault(user => user.Name == userName);
            var tariff = Tariffs.
                FirstOrDefault(tariff => tariff.Key == tariffName);
            
            if (user != null && !tariff.Equals(default(KeyValuePair<string, decimal>)))
            {
                var serviceUsage = new ServiceUsage(user, new Tariff(tariff.Key, tariff.Value), amount);
                ServiceUsages.Add(serviceUsage);
                ChangeServiceUsagesCollection(serviceUsage);
            }
            
            if (user == null)
                throw new ArgumentException($"User '{userName}' not found");

            if (tariff.Equals(default(KeyValuePair<string, decimal>)))
                throw new ArgumentException($"Tariff '{tariffName}' not found");
        }
        
        
        public List<string> GetTariffNamesSortedByCost()
        {
            return Tariffs
                .OrderByDescending(tariff => tariff.Value)
                .Select(tariff => tariff.Key)
                .ToList();
        }
        
        public decimal GetTotalCost()
        {
            return ServiceUsages.Sum(serviceUsage => serviceUsage.CalculateCost());
        }
        
        public decimal GetTotalCostForUser(string userName)
        {
            return ServiceUsages
                .Where(serviceUsage =>  serviceUsage.User.Name == userName)
                .Sum(serviceUsage => serviceUsage.CalculateCost());
        }
        
        public string? GetUsernameWithMaxPayment()
        {
            return ServiceUsages
                .GroupBy(su => su.User)
                .Select(group => new
                {
                    User = group.Key,
                    TotalCost = group.Sum(su => su.CalculateCost())
                })
                .OrderByDescending(x => x.TotalCost) 
                .FirstOrDefault()?.User.Name; 
            
        }
        
        public int GetUserCountWithCostGreaterThan(decimal amount)
        {
            var userServices = ServiceUsages
                .GroupBy(su => su.User)
                .Select(group => new
                {
                    User = group.Key,
                    TotalCost = group.Sum(su => su.CalculateCost())
                });
            
                //
                
            return userServices.Aggregate(0, (acc, ut) => ut.TotalCost > amount ? acc + 1 : acc);
        }
        
        public void GetUserPaymentsForTariffs()
        {
            var userPayments = ServiceUsages
                .GroupBy(su => su.User)
                .Select(group => new
                {
                    User = group.Key,
                    Payments = group.Select(su => su.CalculateCost()).ToList()
                });
            

            foreach (var user in userPayments)
            {
                Console.WriteLine($"Пользователь: {user.User.Name}");
                Console.WriteLine("Оплата  по каждой услуге: " + string.Join(", ", user.Payments));
            }
        }
    }
}