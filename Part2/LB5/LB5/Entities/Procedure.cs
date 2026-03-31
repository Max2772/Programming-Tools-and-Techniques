using SQLite;

namespace LB5.Entities;

[Table("Procedures")]
public class Procedure
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    public string Name { get; set; }
    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }

    [Indexed] 
    public int ProcedureTypeId { get; set; }
}