namespace _453502_Bibikau_Lab2;

public class Passenger
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool HasBaggage { get; set; }

    public Passenger(string name, bool hasBaggage)
    {
        Id = Random.Shared.NextInt64();
        Name = name;
        HasBaggage = hasBaggage;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, HasBaggage: {HasBaggage}";
    }
}