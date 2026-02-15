namespace Bibikau.Mediator;

/// <summary>
/// Запрос, не возвращающий данные
/// </summary>
public interface IRequest { }

/// <summary>
/// Запрос, возвращающий данные
/// </summary>
/// <typeparam name="TResponse">Тип возвращаемых данных</typeparam>
public interface IRequest<out TResponse> { }
