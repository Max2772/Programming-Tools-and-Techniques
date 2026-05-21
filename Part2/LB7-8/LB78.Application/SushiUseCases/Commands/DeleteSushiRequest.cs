namespace LB78.Application.SushiUseCases.Commands;

public sealed record DeleteSushiRequest(Sushi Sushi) : IRequest;

internal class DeleteSushiRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteSushiRequest>
{
    public async Task Handle(DeleteSushiRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.SushiRepository.DeleteAsync(request.Sushi, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
