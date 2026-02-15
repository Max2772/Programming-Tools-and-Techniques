using _453502_Bibikau_Lab4;
namespace _453502_Bibikau_Lab4;

public interface IFileService
{
    IEnumerable<Passenger> ReadFile(string fileName);
    void SaveData(IEnumerable<Passenger> data, string fileName);
}