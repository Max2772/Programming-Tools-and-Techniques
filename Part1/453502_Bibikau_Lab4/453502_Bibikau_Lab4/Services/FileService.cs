using _453502_Bibikau_Lab4;

namespace _453502_Bibikau_Lab4;

public class FileService : IFileService
{
    public IEnumerable<Passenger> ReadFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"File '{fileName}' does not exist");
            yield break;    
        }

        using var reader = new BinaryReader(File.Open(fileName, FileMode.Open));
        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            string name = reader.ReadString();
            double luggageWeight = reader.ReadDouble();
            yield return new Passenger(name, luggageWeight);
        }
        
    }

    public void SaveData(IEnumerable<Passenger> data, string fileName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using var writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
            foreach (var item in data)
            {
                writer.Write(item.Name);
                writer.Write(item.LuggageWeight);
            }
        }
        catch(IOException ex)
        {
            Console.WriteLine($"Error writing file: {ex.Message}");
        }
        
    }
}