using Microsoft.Extensions.Configuration;
using Hospital.Domain;
using SerializerLib;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

string fileName = configuration.GetValue<string>("LabSettings:FileName") ?? "453502_Bibikau_Lab5";

string outputDir = configuration.GetValue<string>("LabSettings:OutputDirectory") 
    ?? AppDomain.CurrentDomain.BaseDirectory;

if (!Directory.Exists(outputDir))
{
    Directory.CreateDirectory(outputDir);
}

string jsonFileName = Path.Combine(outputDir, fileName + "(JSON)");
string xmlFileName = Path.Combine(outputDir, fileName + "(XML)");
string linqFileName = Path.Combine(outputDir, fileName + "(LINQ)");

var hospitals = new List<Hospital.Domain.Hospital>
{
    new Hospital.Domain.Hospital("Минская городская клиническая больница №1", "ул. Интернациональная, 35", "+375 17 229-23-45")
    {
        Departments = new List<Department>
        {
            new Department(1, "Кардиология", "Др. Иванов", "+375 17 229-23-46", 25, 60),
            new Department(2, "Терапия", "Др. Петрова", "+375 17 229-23-47", 30, 80)
        }
    },
    new Hospital.Domain.Hospital("Минская городская детская больница №2", "ул. Сурганова, 53", "+375 17 223-11-22")
    {
        Departments = new List<Department>
        {
            new Department(3, "Педиатрия", "Др. Смирнова", "+375 17 223-11-23", 20, 50),
            new Department(4, "Неврология", "Др. Козлов", "+375 17 223-11-24", 15, 40)
        }
    },
    new Hospital.Domain.Hospital("Республиканский научно-практический центр онкологии и медицинской радиологии", "ул. Волгоградская, 19", "+375 17 247-55-66")
    {
        Departments = new List<Department>
        {
            new Department(5, "Онкология", "Др. Лебедев", "+375 17 247-55-67", 40, 100),
            new Department(6, "Лучевая терапия", "Др. Алексеева", "+375 17 247-55-68", 35, 80)
        }
    }
};


ISerializer serializer = new Serializer();
serializer.SerializeJSON(hospitals, jsonFileName);
serializer.SerializeXML(hospitals, xmlFileName);
serializer.SerializeByLINQ(hospitals, linqFileName);

var jsonHospitals = serializer.DeSerializeJSON(jsonFileName);
var xmlHospitals = serializer.DeSerializeXML(xmlFileName);
var linqHospitals = serializer.DeSerializeByLINQ(linqFileName);

bool linqMatch = hospitals.SequenceEqual(linqHospitals);
bool xmlMatch = hospitals.SequenceEqual(xmlHospitals);
bool jsonMatch = hospitals.SequenceEqual(jsonHospitals);

Console.WriteLine($"LINQ Match: {linqMatch}");
Console.WriteLine($"XML Match: {xmlMatch}");
Console.WriteLine($"JSON Match: {jsonMatch}");
