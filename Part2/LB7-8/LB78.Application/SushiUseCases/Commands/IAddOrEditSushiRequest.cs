namespace LB78.Application.SushiUseCases.Commands;

public interface IAddOrEditSushiRequest : IRequest
{
    Sushi Sushi { get; set; }
}
