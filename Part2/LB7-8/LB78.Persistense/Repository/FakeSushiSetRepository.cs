using System.Linq.Expressions;

namespace LB78.Persistense.Repository;

public class FakeSushiSetRepository : IRepository<SushiSet>
{
    private readonly List<SushiSet> _sushiSets;

    public FakeSushiSetRepository()
    {
        _sushiSets =
        [
            new()
            {
                Id = 1,
                Name = "Классический",
                Price = 25.50m,
                PhotoPath = string.Empty,
                SushiList = []
            },
            new()
            {
                Id = 2,
                Name = "Премиум",
                Price = 42.00m,
                PhotoPath = string.Empty,
                SushiList = []
            },
            new()
            {
                Id = 3,
                Name = "Вегетарианский",
                Price = 18.90m,
                PhotoPath = string.Empty,
                SushiList = []
            }
        ];
    }

    public async Task<IReadOnlyList<SushiSet>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => (IReadOnlyList<SushiSet>)_sushiSets.AsReadOnly(), cancellationToken);
    }

    public Task<SushiSet> GetByIdAsync(int id, CancellationToken cancellationToken = default,
        params Expression<Func<SushiSet, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SushiSet>> ListAsync(Expression<Func<SushiSet, bool>> filter,
        CancellationToken cancellationToken = default, params Expression<Func<SushiSet, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(SushiSet entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SushiSet entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(SushiSet entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SushiSet> FirstOrDefaultAsync(Expression<Func<SushiSet, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
