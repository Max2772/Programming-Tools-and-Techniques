using LB6.Entities.Database;

namespace LB6.Services.Database;

public interface IDbService
{
    IEnumerable<ProcedureType> GetAllProcedureTypes();
    IEnumerable<Procedure> GetProceduresByType(int typeId);
    void Init();
}
