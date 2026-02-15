using _453502_Bibikau_Lab4;

string projectDirectory = Directory.GetCurrentDirectory();
string folderPath = Path.Combine(projectDirectory, "Бибиков_Lab4");

if (Directory.Exists(folderPath))
{
    Directory.Delete(folderPath, true);
    Console.WriteLine($"Папка {folderPath} удалена.");
}
Directory.CreateDirectory(folderPath);
Console.WriteLine($"Папка {folderPath} успешно создана.");

Random random = new();
List<string> extensions = new() { ".txt", ".rtf", ".dat", ".inf" };
for (int i = 0; i < 10; i++)
{
    string randomFileName = Path.Combine(
        folderPath, 
        Path.GetRandomFileName().Replace(".", "") + extensions[random.Next(extensions.Count)]
        );
    File.Create(randomFileName).Dispose();
}

string[] files = Directory.GetFiles(folderPath);
foreach (var file in files)
{
    Console.WriteLine($"Файл: {Path.GetFileName(file)} имеет расширение {Path.GetExtension(file)}");
}

List<Passenger> passengers = new()
{
    new Passenger("Максим", 10),
    new Passenger("Мария", 18),
    new Passenger("Олег", 22),
    new Passenger("Светлана", 12),
    new Passenger("Дмитрий", 35),
    new Passenger("Наталья", 28)
};

FileService fileService = new();
string dataFileName = Path.Combine(folderPath, "PassengerData.dat");
fileService.SaveData(passengers, dataFileName);

string newDataFileName = Path.Combine(folderPath, "renamedPassengerData.dat");
File.Move(dataFileName, newDataFileName);

List<Passenger> loadedPassengers = fileService.ReadFile(newDataFileName).ToList();

Console.WriteLine("Исходная коллекция:");
foreach (var passenger in passengers)
{
    Console.WriteLine(passenger);
}

var sortedByName = loadedPassengers.OrderBy(p => p, new MyCustomComparer()).ToList();
Console.WriteLine("Отсортированная коллекция по имени:");
foreach (var passenger in sortedByName)
{
    Console.WriteLine(passenger);
}

var sortedByWeight = loadedPassengers.OrderBy(p => p.LuggageWeight).ToList();
Console.WriteLine("Отсортированная коллекция по весу багажа:");
foreach (var passenger in sortedByWeight)
{
    Console.WriteLine(passenger);
}   