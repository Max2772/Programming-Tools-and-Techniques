namespace LB78.Application.SushiUseCases.Commands;

public sealed class AddSushiRequest : IAddOrEditSushiRequest
{
    public Sushi Sushi { get; set; } = null!;
}

internal class AddSushiRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddSushiRequest>
{
    public async Task Handle(AddSushiRequest request, CancellationToken cancellationToken)
    {
        if (request.Sushi.Id == 0)
        {
            var all = await unitOfWork.SushiRepository.ListAllAsync(cancellationToken);
            request.Sushi.Id = all.Count > 0 ? all.Max(s => s.Id) + 1 : 1;
        }

        await unitOfWork.SushiRepository.AddAsync(request.Sushi, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
