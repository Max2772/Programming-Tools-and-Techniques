namespace LB78.Application.SushiSetUseCases.Queries;

public sealed record GetByIdSushiSetRequest(int Id) : IRequest<SushiSet?>;

internal class GetByIdSushiSetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdSushiSetRequest, SushiSet?>
{
    public async Task<SushiSet?> Handle(GetByIdSushiSetRequest request, CancellationToken cancellationToken)
    {
        return await unitOfWork.SushiSetRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
