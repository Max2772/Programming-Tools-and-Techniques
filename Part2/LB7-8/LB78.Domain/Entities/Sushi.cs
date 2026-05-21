namespace LB78.Domain.Entities;

public class Sushi : Entity
{
    public int SushiSetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ReadyCount { get; set; }
    public decimal Weight { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public SushiSet? SushiSet { get; set; }
}
