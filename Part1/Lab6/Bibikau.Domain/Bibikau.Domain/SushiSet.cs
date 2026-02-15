namespace Bibikau.Domain;
    
[Serializable]
public class SushiSet : IEquatable<SushiSet>
{
    public int Id { get; set; }
    public string Name { get; set; }  
    public bool Limited { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public float Protein { get; set; }   
    public float Fats { get; set; }      
    public float Carbs { get; set; }     
    public float Weight { get; set; }    

    public SushiSet()
    {
        Id = 0;
        Name = string.Empty;
        Limited = false;
        Description = string.Empty;
        Price = 0;
        Protein = 0;
        Fats = 0;
        Carbs = 0;
        Weight = 0;
    }

    public SushiSet(int id, string name, bool limited, string description, decimal price,
                    float protein, float fats, float carbs, float weight)
    {
        Id = id;
        Name = name;
        Limited = limited;
        Description = description;
        Price = price;
        Protein = protein;
        Fats = fats;
        Carbs = carbs;
        Weight = weight;
    }

    public override string ToString()
    {
        return
            $"ID: {Id}\n" +
            $"Название: {Name}\n" +
            $"Лимитированный: {Limited}\n" +
            $"Описание: {Description}\n" +
            $"Цена: {Price} руб.\n" +
            $"Белки: {Protein}г\n" +
            $"Жиры: {Fats}г\n" +
            $"Углеводы: {Carbs}г\n" +
            $"Вес: {Weight}г\n" +
            "---\n";
    }

    public bool Equals(SushiSet? other)
    {
        if (other == null) return false;
        
        const float eps = 0.01f;

        return Id == other.Id
               && Name == other.Name
               && Limited == other.Limited
               && Description == other.Description
               && Price == other.Price
               && Math.Abs(Protein - other.Protein) < eps
               && Math.Abs(Fats - other.Fats) < eps
               && Math.Abs(Carbs - other.Carbs) < eps
               && Math.Abs(Weight - other.Weight) < eps;
    }
}