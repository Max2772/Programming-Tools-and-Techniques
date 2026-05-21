namespace LB78.Application.SushiSetUseCases.Queries;

public sealed record GetAllSushiSetRequest() : IRequest<IEnumerable<SushiSet>>;

internal class GetAllSushiSetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSushiSetRequest, IEnumerable<SushiSet>>
{
    public async Task<IEnumerable<SushiSet>> Handle(
        GetAllSushiSetRequest request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.SushiSetRepository.ListAllAsync(cancellationToken);
    }
}
