using System.Reflection;
using Bibikau.Domain;
using Bibikau.Mediator;
using Bibikau.UseCases;

var sushiSets = new List<SushiSet>
{
    new ()
    { 
        Id = 1, 
        Name = "Филадельфия", 
        Limited = false, 
        Description = "Классические роллы с лососем", 
        Price = 12.5m,
        Protein = 15.5f,
        Fats = 12.3f,
        Carbs = 45.2f,
        Weight = 250f
    },
    new () 
    { 
        Id = 2, 
        Name = "Калифорния", 
        Limited = true, 
        Description = "Роллы с крабом и авокадо", 
        Price = 17.5m,
        Protein = 12.8f,
        Fats = 8.7f,
        Carbs = 38.9f,
        Weight = 220f
    },
    new () 
    { 
        Id = 3, 
        Name = "Острый тунец", 
        Limited = false, 
        Description = "Острые роллы с тунцом", 
        Price = 15m,
        Protein = 18.2f,
        Fats = 9.5f,
        Carbs = 42.7f,
        Weight = 280f
    }
};

Assembly useCasesAssembly = Assembly.GetAssembly(typeof(SaveData)) ?? throw new InvalidOperationException();
Sender sender = new Sender(useCasesAssembly);

sender.Send(new SaveData(sushiSets, "sushiSets.json"));
Console.WriteLine("Данные сохранены в файл");

var sushiSetsFromFile = sender.Send(new ReadFile("sushiSets.json")).ToList();

Console.WriteLine("\nДанные из файла:");
foreach (var sushiSet in sushiSetsFromFile)
{
    Console.WriteLine(sushiSet);
}

bool areEqual = sushiSets.SequenceEqual(sushiSetsFromFile);
Console.WriteLine($"\nКоллекции идентичны: {areEqual}");