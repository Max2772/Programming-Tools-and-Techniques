using LB6.Entities;

namespace LB6.Services;

public interface IDbService
{
    IEnumerable<ProcedureType> GetAllProcedureTypes();
    IEnumerable<Procedure> GetProceduresByType(int typeId);
    void Init();
}
