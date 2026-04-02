using LB5.Entities;
using SQLite;

namespace LB5.Services;

public class SQLiteService : IDbService
{
    private SQLiteConnection _db;
    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "Spa.db");

    private bool TableExists(string tableName)
    {
        var tableInfo = _db.GetTableInfo(tableName);
        return tableInfo.Any();
    }

    public IEnumerable<ProcedureType> GetAllProcedureTypes() =>
        _db.Table<ProcedureType>().ToList();

    public IEnumerable<Procedure> GetProceduresByType(int typeId) =>
        _db.Table<Procedure>()
            .Where(p => p.ProcedureTypeId == typeId)
            .ToList();

    public void Init()
    {
        _db = new SQLiteConnection(_dbPath);

        var procedureTypes = new List<ProcedureType>
        {
            new() { Name = "Массаж" },
            new() { Name = "Уход за лицом" },
            new() { Name = "Уход за телом" },
            new() { Name = "Маникюр и педикюр" },
        };

        if (TableExists("ProcedureTypes") && TableExists("Procedures"))
        {
            _db.DeleteAll<ProcedureType>();
            _db.DeleteAll<Procedure>();
            
            foreach (var type in procedureTypes)
            {
                _db.Insert(type);
            }
            
            var procedures = BuildProcedureList(procedureTypes);
            foreach (var procedure in procedures)
                _db.Insert(procedure);
            
        }
        else
        {
            _db.CreateTable<ProcedureType>();
            _db.CreateTable<Procedure>();

            foreach (var type in procedureTypes)
            {
                _db.Insert(type);
            }

            var procedures = BuildProcedureList(procedureTypes);
            foreach (var procedure in procedures)
            {
                _db.Insert(procedure);
            }
        }
    }

    private List<Procedure> BuildProcedureList(List<ProcedureType> types)
    {
        return new List<Procedure>
        {
            new() { Name = "Классический массаж", DurationMinutes = 60, Price = 45m, ProcedureTypeId = types[0].Id },
            new() { Name = "Тайский массаж", DurationMinutes = 90, Price = 70m, ProcedureTypeId = types[0].Id },
            new() { Name = "Горячие камни", DurationMinutes = 75, Price = 80m, ProcedureTypeId = types[0].Id },
            new() { Name = "Антицеллюлитный массаж", DurationMinutes = 60, Price = 55m, ProcedureTypeId = types[0].Id },
            new() { Name = "Лимфодренажный массаж", DurationMinutes = 50, Price = 60m, ProcedureTypeId = types[0].Id },
            new() { Name = "Расслабляющий массаж", DurationMinutes = 45, Price = 40m, ProcedureTypeId = types[0].Id },

            new() { Name = "Чистка лица", DurationMinutes = 60, Price = 38m, ProcedureTypeId = types[1].Id },
            new() { Name = "Гидрофациал", DurationMinutes = 60, Price = 110m, ProcedureTypeId = types[1].Id },
            new() { Name = "Химический пилинг", DurationMinutes = 45, Price = 90m, ProcedureTypeId = types[1].Id },
            new() { Name = "Лифтинг-маска", DurationMinutes = 40, Price = 55m, ProcedureTypeId = types[1].Id },
            new() { Name = "Мезотерапия лица", DurationMinutes = 50, Price = 125m, ProcedureTypeId = types[1].Id },
            new() { Name = "Биоревитализация", DurationMinutes = 40, Price = 140m, ProcedureTypeId = types[1].Id },

            new() { Name = "Обёртывание шоколадом", DurationMinutes = 60, Price = 75m, ProcedureTypeId = types[2].Id },
            new() { Name = "Солевой скраб", DurationMinutes = 45, Price = 50m, ProcedureTypeId = types[2].Id },
            new() { Name = "Грязевое обёртывание", DurationMinutes = 60, Price = 65m, ProcedureTypeId = types[2].Id },
            new() { Name = "Ароматерапия", DurationMinutes = 50, Price = 60m, ProcedureTypeId = types[2].Id },
            new() { Name = "Гидромассажная ванна", DurationMinutes = 30, Price = 35m, ProcedureTypeId = types[2].Id },
            new() { Name = "Медовое обёртывание", DurationMinutes = 55, Price = 68m, ProcedureTypeId = types[2].Id },

            new() { Name = "Классический маникюр", DurationMinutes = 60, Price = 25m, ProcedureTypeId = types[3].Id },
            new() { Name = "Аппаратный маникюр", DurationMinutes = 75, Price = 32m, ProcedureTypeId = types[3].Id },
            new() { Name = "Гель-лак", DurationMinutes = 90, Price = 45m, ProcedureTypeId = types[3].Id },
            new() { Name = "Классический педикюр", DurationMinutes = 60, Price = 30m, ProcedureTypeId = types[3].Id },
            new() { Name = "Спа-педикюр", DurationMinutes = 80, Price = 48m, ProcedureTypeId = types[3].Id },
            new() { Name = "Наращивание ногтей", DurationMinutes = 120, Price = 80m, ProcedureTypeId = types[3].Id },
        };
    }
}