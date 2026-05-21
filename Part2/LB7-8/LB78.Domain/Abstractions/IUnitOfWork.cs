using LB78.Domain.Entities;

namespace LB78.Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<SushiSet> SushiSetRepository { get; }
    IRepository<Sushi> SushiRepository { get; }

    Task SaveAllAsync();
    Task DeleteDataBaseAsync();
    Task CreateDataBaseAsync();
}
