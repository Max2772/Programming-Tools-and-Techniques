namespace Bibikau.Mediator;

/// <summary>
/// Обработчик запроса
/// </summary>
/// <typeparam name="TRequest">Тип запроса</typeparam>
public interface IRequestHandler<in TRequest> where TRequest : IRequest
{
    void Handle(TRequest request);
}

/// <summary>
/// Обработчик запроса, возвращающий данные
/// </summary>
/// <typeparam name="TRequest">Тип запроса</typeparam>
/// <typeparam name="TResponse">Тип возвращаемых данных</typeparam>
public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    TResponse Handle(TRequest request);
}