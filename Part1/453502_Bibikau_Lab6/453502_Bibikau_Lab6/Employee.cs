namespace _453502_Bibikau_Lab6;

public class Employee
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsMarried { get; set; }

    public override string ToString()
    {
        return $"Age: {Age}, Name: {Name}, IsMarried: {(IsMarried ? "Yes" : "No")}";
    }
}