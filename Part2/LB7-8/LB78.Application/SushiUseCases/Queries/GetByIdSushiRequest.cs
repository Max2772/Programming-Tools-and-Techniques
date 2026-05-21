namespace LB78.Application.SushiUseCases.Queries;

public sealed record GetByIdSushiRequest(int Id) : IRequest<Sushi?>;

internal class GetByIdSushiRequestHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<GetByIdSushiRequest, Sushi?>
{
    public async Task<Sushi?> Handle(GetByIdSushiRequest request, CancellationToken cancellationToken)
    {
        return await unitOfWork.SushiRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
