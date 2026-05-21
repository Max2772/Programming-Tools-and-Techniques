using LB78.Persistense.Data;
using LB78.Persistense.Repository;

namespace LB78.Persistense.UnitOfWork;

public class EfUnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly Lazy<IRepository<Sushi>> _sushiRepository = new(() => new EfRepository<Sushi>(context));
    private readonly Lazy<IRepository<SushiSet>> _sushiSetRepository = new(() => new EfRepository<SushiSet>(context));

    public IRepository<Sushi> SushiRepository => _sushiRepository.Value;
    public IRepository<SushiSet> SushiSetRepository => _sushiSetRepository.Value;

    public async Task SaveAllAsync() => await context.SaveChangesAsync();

    public async Task DeleteDataBaseAsync() => await context.Database.EnsureDeletedAsync();

    public async Task CreateDataBaseAsync() => await context.Database.EnsureCreatedAsync();
}
