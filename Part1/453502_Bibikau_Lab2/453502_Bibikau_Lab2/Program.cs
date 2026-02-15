using System.Numerics;
using _453502_Bibikau_Lab2.Entities;

namespace _453502_Bibikau_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            HousingServiceProvider<decimal> provider = new();
            Journal<decimal> journal = new();

            provider.TariffAdded += journal.TariffAdd;
            provider.TariffRemoved += journal.TariffRemove;
            provider.UserAdded += journal.UserAdd;
            provider.UserRemoved += journal.UserRemove;
            provider.ChangeServiceUsagesCollection += (serviceUsage) =>
            {
                Console.WriteLine($"{serviceUsage.User.Name} подключил {serviceUsage.Tariff.Name}!");
            };

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
            
            journal.ShowEvents();
            
            
            // NullReferenceException => Add(T item)
            try
            {
                provider.AddTariff(null);
            }
            catch (NullReferenceException ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            
            // NullReferenceException => Add(T item)
            try
            {
                provider.AddUser(null); 
            }
            catch (NullReferenceException ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            
            // ItemNotFoundException => Remove(T item)
            try
            {
                provider.AddTariff(new Tariff<decimal>("Test", 100));
                var nonExistentTariff = new Tariff<decimal>("NonExistent", 6969);
                provider.RemoveTariff(nonExistentTariff); 
            }
            catch (ItemNotFoundException ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
            
            // ArgumentException => AddServiceUsage(string userName, string tariffName, T amount)
            try { provider.AddServiceUsage("NonExistentUser", "NonExistentTariff", 10); }
            catch (ArgumentException ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }

            //   
            try
            {
                provider.AddUser(new User(123, "Иван"));
                provider.AddTariff(new Tariff<decimal>("Услуга", 100));
                provider.AddServiceUsage("Иван", "Услуга", 10);
                var services = provider.GetServicesForUser("Иван");
                var service = services[5];
            }
            catch (IndexOutOfRangeException ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
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