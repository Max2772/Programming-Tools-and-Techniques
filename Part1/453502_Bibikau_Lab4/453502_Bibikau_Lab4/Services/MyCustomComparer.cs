using _453502_Bibikau_Lab4;

namespace _453502_Bibikau_Lab4;

public class MyCustomComparer : IComparer<Passenger>
{
    public int Compare(Passenger? x, Passenger? y)
    {
        if (x == null) throw new ArgumentNullException(nameof(x));
        if (y == null) throw new ArgumentNullException(nameof(y));

        return string.Compare(x.Name, y.Name, System.StringComparison.OrdinalIgnoreCase);
    }
}