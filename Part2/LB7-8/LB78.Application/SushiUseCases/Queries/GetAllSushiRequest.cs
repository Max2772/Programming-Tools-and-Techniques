namespace LB78.Application.SushiUseCases.Queries;

public sealed record GetAllSushiRequest() : IRequest<IEnumerable<Sushi>>;

internal class GetAllSushiRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSushiRequest,
    IEnumerable<Sushi>>
{
    public async Task<IEnumerable<Sushi>> Handle(
        GetAllSushiRequest request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.SushiRepository.ListAllAsync(cancellationToken);
    }
}
