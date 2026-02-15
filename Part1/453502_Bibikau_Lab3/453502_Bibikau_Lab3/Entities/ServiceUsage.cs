namespace _453502_Bibikau_Lab3
{
    public class ServiceUsage
    {
        public User User { get; set; }
        public Tariff Tariff { get; set; }
        public decimal Amount { get; set; }

        public ServiceUsage(User user, Tariff tariff, decimal amount)
        {
            User = user;
            Tariff = tariff;
            Amount = amount;
        }
        
        public decimal CalculateCost() => Tariff.Price * Amount; 
    }
}