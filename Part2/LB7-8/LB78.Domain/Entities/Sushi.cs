namespace LB78.Domain.Entities;

public class Sushi : Entity
{
    public Sushi()
    {
    }

    public Sushi(string name, int readyCount, decimal weight, string description, int sushiSetId)
    {
        Name = name;
        ReadyCount = readyCount;
        Weight = weight;
        Description = description;
        SushiSetId = sushiSetId;
    }

    public int SushiSetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ReadyCount { get; set; }
    public decimal Weight { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public SushiSet? SushiSet { get; set; }

    public void ChangeReadyCount(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Количество готовых суши не может быть отрицательным.");
        ReadyCount = count;
    }

    public void AttachToSet(int setId) => SushiSetId = setId;
}
