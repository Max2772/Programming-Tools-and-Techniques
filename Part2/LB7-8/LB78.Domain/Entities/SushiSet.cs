namespace LB78.Domain.Entities;

public class SushiSet : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<Sushi> SushiList { get; set; } = new();
}
