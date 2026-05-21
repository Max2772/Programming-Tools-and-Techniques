namespace LB78.Application.SushiUseCases.Commands;

public sealed class EditSushiRequest : IAddOrEditSushiRequest
{
    public Sushi Sushi { get; set; } = null!;
}

internal class EditSushiRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditSushiRequest>
{
    public async Task Handle(EditSushiRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.SushiRepository.UpdateAsync(request.Sushi, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
