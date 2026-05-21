using System.Linq.Expressions;

namespace LB78.Persistense.Repository;

public class FakeSushiRepository : IRepository<Sushi>
{
    private readonly List<Sushi> _sushi = new();

    public FakeSushiRepository()
    {
        Random random = new();
        for (int i = 1; i <= 3; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                _sushi.Add(new Sushi()
                {
                    Id = (i - 1) * 10 + (j + 1),
                    Name = $"Суши {(j + 1) + (i - 1) * 10}",
                    ReadyCount = random.Next(0, 10),
                    Weight = random.Next(30, 120),
                    Description = "Рис, нори, начинка",
                    SushiSetId = i
                });
            }
        }
    }

    public async Task<IReadOnlyList<Sushi>> ListAsync(Expression<Func<Sushi, bool>>? filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<Sushi, object>>[]? includesProperties)
    {
        IEnumerable<Sushi> query = _sushi;
        if (filter != null)
        {
            query = query.Where(filter.Compile());
        }

        return await Task.FromResult(query.ToList());
    }

    public Task AddAsync(Sushi entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Sushi entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Sushi> FirstOrDefaultAsync(Expression<Func<Sushi, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Sushi> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Sushi, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Sushi>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Sushi entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
