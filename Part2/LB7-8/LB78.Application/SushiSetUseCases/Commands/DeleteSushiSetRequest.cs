namespace LB78.Application.SushiSetUseCases.Commands;

public sealed record DeleteSushiSetRequest(SushiSet SushiSet) : IRequest;

internal class DeleteSushiSetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteSushiSetRequest>
{
    public async Task Handle(DeleteSushiSetRequest request, CancellationToken cancellationToken)
    {
        var sushiInSet = await unitOfWork.SushiRepository.ListAsync(s => s.SushiSetId == request.SushiSet.Id, cancellationToken);
        foreach (var item in sushiInSet)
        {
            await unitOfWork.SushiRepository.DeleteAsync(item, cancellationToken);
        }

        await unitOfWork.SushiSetRepository.DeleteAsync(request.SushiSet, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
