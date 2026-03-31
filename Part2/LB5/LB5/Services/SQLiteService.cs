using LB5.Entities;
using SQLite;

namespace LB5.Services;

public class SQLiteService : IDbService
{
    private SQLiteConnection _db;
    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, "Spa.db");

    private bool AreListsEqual<T>(List<T> list1, List<T> list2, Func<T, T, bool> equalityComparer)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; ++i)
        {
            if (!equalityComparer(list1[i], list2[i]))
                return false;
        }

        return true;
    }

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
            var existingTypes = _db.Table<ProcedureType>().ToList();
            var existingProcedures = _db.Table<Procedure>().ToList();
            bool typesWereChanged = false;

            if (!AreListsEqual(procedureTypes, existingTypes, (t1, t2) => t1.Name == t2.Name))
            {
                typesWereChanged = true;
                _db.DeleteAll<ProcedureType>();
                foreach (var type in procedureTypes)
                {
                    _db.Insert(type);
                }
            }

            var procedures = BuildProcedureList(procedureTypes, existingTypes, typesWereChanged);

            if (!AreListsEqual(procedures, existingProcedures, (p1, p2) =>
                    p1.Name == p2.Name &&
                    p1.DurationMinutes == p2.DurationMinutes &&
                    p1.Price == p2.Price &&
                    p1.ProcedureTypeId == p2.ProcedureTypeId))
            {
                _db.DeleteAll<Procedure>();
                foreach (var procedure in procedures)
                {
                    _db.Insert(procedure);
                }
            }
        }
        else
        {
            _db.CreateTable<ProcedureType>();
            _db.CreateTable<Procedure>();

            foreach (var type in procedureTypes)
            {
                _db.Insert(type);
            }

            var procedures = BuildProcedureList(procedureTypes, procedureTypes, false);
            foreach (var procedure in procedures)
            {
                _db.Insert(procedure);
            }
        }
    }

    private List<Procedure> BuildProcedureList(
        List<ProcedureType> newTypes,
        List<ProcedureType> existingTypes,
        bool useNew)
    {
        int Id(int index) => useNew ? newTypes[index].Id : existingTypes[index].Id;

        return new List<Procedure>
        {
            new() { Name = "Классический массаж", DurationMinutes = 60, Price = 45m, ProcedureTypeId = Id(0) },
            new() { Name = "Тайский массаж", DurationMinutes = 90, Price = 70m, ProcedureTypeId = Id(0) },
            new() { Name = "Горячие камни", DurationMinutes = 75, Price = 80m, ProcedureTypeId = Id(0) },
            new() { Name = "Антицеллюлитный массаж", DurationMinutes = 60, Price = 55m, ProcedureTypeId = Id(0) },
            new() { Name = "Лимфодренажный массаж", DurationMinutes = 50, Price = 60m, ProcedureTypeId = Id(0) },
            new() { Name = "Расслабляющий массаж", DurationMinutes = 45, Price = 40m, ProcedureTypeId = Id(0) },

            new() { Name = "Чистка лица", DurationMinutes = 60, Price = 38m, ProcedureTypeId = Id(1) },
            new() { Name = "Гидрофациал", DurationMinutes = 60, Price = 110m, ProcedureTypeId = Id(1) },
            new() { Name = "Химический пилинг", DurationMinutes = 45, Price = 90m, ProcedureTypeId = Id(1) },
            new() { Name = "Лифтинг-маска", DurationMinutes = 40, Price = 55m, ProcedureTypeId = Id(1) },
            new() { Name = "Мезотерапия лица", DurationMinutes = 50, Price = 125m, ProcedureTypeId = Id(1) },
            new() { Name = "Биоревитализация", DurationMinutes = 40, Price = 140m, ProcedureTypeId = Id(1) },

            new() { Name = "Обёртывание шоколадом", DurationMinutes = 60, Price = 75m, ProcedureTypeId = Id(2) },
            new() { Name = "Солевой скраб", DurationMinutes = 45, Price = 50m, ProcedureTypeId = Id(2) },
            new() { Name = "Грязевое обёртывание", DurationMinutes = 60, Price = 65m, ProcedureTypeId = Id(2) },
            new() { Name = "Ароматерапия", DurationMinutes = 50, Price = 60m, ProcedureTypeId = Id(2) },
            new() { Name = "Гидромассажная ванна", DurationMinutes = 30, Price = 35m, ProcedureTypeId = Id(2) },
            new() { Name = "Медовое обёртывание", DurationMinutes = 55, Price = 68m, ProcedureTypeId = Id(2) },

            new() { Name = "Классический маникюр", DurationMinutes = 60, Price = 25m, ProcedureTypeId = Id(3) },
            new() { Name = "Аппаратный маникюр", DurationMinutes = 75, Price = 32m, ProcedureTypeId = Id(3) },
            new() { Name = "Гель-лак", DurationMinutes = 90, Price = 45m, ProcedureTypeId = Id(3) },
            new() { Name = "Классический педикюр", DurationMinutes = 60, Price = 30m, ProcedureTypeId = Id(3) },
            new() { Name = "Спа-педикюр", DurationMinutes = 80, Price = 48m, ProcedureTypeId = Id(3) },
            new() { Name = "Наращивание ногтей", DurationMinutes = 120, Price = 80m, ProcedureTypeId = Id(3) },
        };
    }
}