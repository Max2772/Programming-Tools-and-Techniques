using System.Numerics;

namespace _453502_Bibikau_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            HousingServiceProvider<decimal> provider = new();

            provider.AddTariff(new Tariff<decimal>("Вода", 50.0m));
            provider.AddTariff(new Tariff<decimal>("Электричество", 30.0m));
            provider.AddTariff(new Tariff<decimal>("Газ", 20.0m));

            provider.AddUser(new User(1, "Иванов"));
            provider.AddUser(new User(2, "Петров"));
            provider.AddUser(new User(3, "Сидоров"));

            provider.AddServiceUsage("Иванов", "Вода", 10.0m);
            provider.AddServiceUsage("Иванов", "Электричество", 5.0m);
            provider.AddServiceUsage("Петров", "Газ", 8.0m);
            provider.AddServiceUsage("Сидоров", "Вода", 15.0m);
            
            
            PrintUserServices(provider, "Иванов");
            PrintUserServices(provider, "Петров");
            PrintUserServices(provider, "Сидоров");

            
            Console.WriteLine("Количество заказов для тарифа 'Вода':");
            Console.WriteLine(provider.TotalServicesForTariff("Вода"));
            
            Console.WriteLine("Количество заказов для тарифа 'Электричество':");
            Console.WriteLine(provider.TotalServicesForTariff("Электричество"));
            
            Console.WriteLine("Количество заказов для тарифа 'Газ':");
            Console.WriteLine(provider.TotalServicesForTariff("Газ"));

            
            Console.WriteLine("Общая стоимость всех услуг:");
            Console.WriteLine($"{provider.CalculateTotalCost()} руб");
        }

        private static void PrintUserServices(HousingServiceProvider<decimal> provider, string userName)
        {
            Console.WriteLine($"Тарифы пользователя '{userName}':");
            var services = provider.GetServicesForUser(userName);
            if (services.Count == 0)
            {
                Console.WriteLine("У данного пользователя нет подключенных услуг");
            }
            else
            {
                for (int i = 0; i < services.Count; i++)
                {
                    Console.WriteLine($"{i+1}) {services[i].Tariff.Name}, кол-во: {services[i].Amount}, стоимость: {services[i].CalculateCost()} руб.");
                }
            }

            Console.WriteLine();
        }
    }
}