namespace LB78.Application.SushiSetUseCases.Commands;

public sealed class AddSushiSetRequest : IAddOrEditSushiSetRequest
{
    public SushiSet SushiSet { get; set; } = null!;
}

internal class AddSushiSetRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddSushiSetRequest>
{
    public async Task Handle(AddSushiSetRequest request, CancellationToken cancellationToken)
    {
        if (request.SushiSet.Id == 0)
        {
            var all = await unitOfWork.SushiSetRepository.ListAllAsync(cancellationToken);
            request.SushiSet.Id = all.Count > 0 ? all.Max(s => s.Id) + 1 : 1;
        }

        await unitOfWork.SushiSetRepository.AddAsync(request.SushiSet, cancellationToken);
        await unitOfWork.SaveAllAsync();
    }
}
