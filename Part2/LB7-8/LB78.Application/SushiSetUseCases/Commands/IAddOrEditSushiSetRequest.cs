namespace LB78.Application.SushiSetUseCases.Commands;

public interface IAddOrEditSushiSetRequest : IRequest
{
    SushiSet SushiSet { get; set; }
}
