using _453502_Bibikau_Lab3;

HousingServiceProvider provider = new();
Journal journal = new();

provider.TariffAdded += journal.TariffAdd;
provider.TariffRemoved += journal.TariffRemove;
provider.UserAdded += journal.UserAdd;
provider.UserRemoved += journal.UserRemove;
provider.ChangeServiceUsagesCollection += (serviceUsage) =>
{
    Console.WriteLine($"{serviceUsage.User.Name} подключил {serviceUsage.Tariff.Name}!");
};

provider.AddTariff(new Tariff("Вода", 50.0m));
provider.AddTariff(new Tariff("Электричество", 30.0m));
provider.AddTariff(new Tariff("Газ", 20.0m));

provider.AddUser(new User(1, "Иванов"));
provider.AddUser(new User(2, "Петров"));
provider.AddUser(new User(3, "Сидоров"));

provider.AddServiceUsage("Иванов", "Вода", 10.0m);
provider.AddServiceUsage("Иванов", "Электричество", 5.0m);
provider.AddServiceUsage("Петров", "Газ", 8.0m);
provider.AddServiceUsage("Сидоров", "Вода", 15.0m);

Console.WriteLine("Тарифы, отсортированные по стоимости:");
Console.WriteLine(string.Join(",", provider.GetTariffNamesSortedByCost()));

Console.WriteLine("Общая стоимость всех услуг:");
Console.WriteLine($"{provider.GetTotalCost()} руб");

Console.WriteLine("Стоимость услуг для Иванова:");
Console.WriteLine($"{provider.GetTotalCostForUser("Иванов")} руб");

Console.WriteLine("Пользователь с максимальной суммой платежей:");
Console.WriteLine(provider.GetUsernameWithMaxPayment() ?? "Нет данных");

Console.WriteLine("Количество пользователей с платежами больше 500 руб:");
Console.WriteLine(provider.GetUserCountWithCostGreaterThan(500.0m));

Console.WriteLine("Платежи пользователей по тарифам:");
provider.GetUserPaymentsForTariffs();

Console.WriteLine("\n\n=== Журнал событий ===");

journal.ShowEvents();