using LB78.Persistense.Repository;

namespace LB78.Persistense.UnitOfWork;

public class FakeUnitOfWork : IUnitOfWork
{
    private readonly FakeSushiSetRepository _fakeSushiSetRepository = new();
    private readonly FakeSushiRepository _fakeSushiRepository = new();

    public IRepository<SushiSet> SushiSetRepository => _fakeSushiSetRepository;

    public IRepository<Sushi> SushiRepository => _fakeSushiRepository;

    public Task SaveAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteDataBaseAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateDataBaseAsync()
    {
        throw new NotImplementedException();
    }
}
