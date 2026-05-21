namespace LB78.Application.SushiUseCases.Queries;

public sealed record GetBySushiSetSushiRequest(int Id) : IRequest<IEnumerable<Sushi>>;

internal class GetBySushiSetSushiRequestHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetBySushiSetSushiRequest, IEnumerable<Sushi>>
{
    public async Task<IEnumerable<Sushi>> Handle(
        GetBySushiSetSushiRequest request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.SushiRepository.ListAsync(x => x.SushiSetId == request.Id, cancellationToken);
    }
}
