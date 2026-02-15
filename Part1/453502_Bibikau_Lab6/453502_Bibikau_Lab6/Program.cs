using System.Reflection;
using _453502_Bibikau_Lab6;

var employees = new List<Employee>
{
    new() { Name = "Алексей", Age = 31, IsMarried = true },
    new() { Name = "Дарья", Age = 25, IsMarried = false },
    new() { Name = "Игорь", Age = 45, IsMarried = true },
    new() { Name = "Марина", Age = 29, IsMarried = false },
    new() { Name = "Сергей", Age = 38, IsMarried = true },
    new() { Name = "Полина", Age = 33, IsMarried = true },
    new() { Name = "Олег", Age = 24, IsMarried = false },
    new() { Name = "Виктория", Age = 27, IsMarried = false },
    new() { Name = "Григорий", Age = 41, IsMarried = true },
    new() { Name = "Наталья", Age = 36, IsMarried = true }
};

string libraryName = "FileService.dll";

string libraryPath = Path.Combine(Directory.GetCurrentDirectory(), libraryName);
Assembly assembly = Assembly.LoadFrom(libraryPath);

Type fileServiceType = assembly.GetType("FileService.FileService`1").MakeGenericType(typeof(Employee));
dynamic fileService = Activator.CreateInstance(fileServiceType);

string fileName = "Employees.json";

fileService.SaveData(employees, fileName);

IEnumerable<Employee> loadedEmployees = fileService.ReadFile(fileName);
foreach (var employee in loadedEmployees)
{
    Console.WriteLine(employee);
}