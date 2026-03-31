using LB5.Entities;

namespace LB5.Services;

public interface IDbService
{
    IEnumerable<ProcedureType> GetAllProcedureTypes();
    IEnumerable<Procedure> GetProceduresByType(int typeId);
    void Init();
}
