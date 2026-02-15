namespace _453502_Bibikau_Lab4;

public class Passenger
{
    public string Name { get; private set; }
    public double LuggageWeight { get; set; }
    public bool IsOverweight => LuggageWeight > 20;

    public Passenger(string name, double luggage)
    {
        Name = name;
        LuggageWeight = luggage;
    }

    public override string ToString()
    {
        return $"Passenger '{Name}', Luggage Weight: {LuggageWeight}, IsOverweight: {IsOverweight}";
    }
}