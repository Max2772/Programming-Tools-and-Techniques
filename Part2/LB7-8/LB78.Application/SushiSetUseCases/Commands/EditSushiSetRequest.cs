namespace LB78.Application.SushiSetUseCases.Commands;

public sealed class EditSushiSetRequest : IAddOrEditSushiSetRequest
{
    public SushiSet SushiSet { get; set; } = null!;
}

internal class EditSushiSetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditSushiSetRequest>
{
    public async Task Handle(EditSushiSetRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.SushiSetRepository.UpdateAsync(request.SushiSet, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
