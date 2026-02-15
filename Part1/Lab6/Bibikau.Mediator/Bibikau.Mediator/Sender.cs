using System.Reflection;

namespace Bibikau.Mediator;

public class Sender : ISender
{
    private readonly Assembly _assembly;
    private readonly Dictionary<Type, Type>? _handlerImplementations = new();

    public Sender(Assembly? assembly = null)
    {
        _assembly = assembly ?? Assembly.GetCallingAssembly();
        RegisterHandlers(_assembly);
    }

    private void RegisterHandlers(Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces(), (t, i) => new { Type = t, Interface = i })
            .Where(x => x.Interface.IsGenericType &&
                       (x.Interface.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                        x.Interface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            _handlerImplementations[handlerType.Interface] = handlerType.Type; 
        }
    }

    public void Send<TRequest>(TRequest request) where TRequest : IRequest
    {
        Type handlerType = typeof(IRequestHandler<>).MakeGenericType(typeof(TRequest));
        try
        {
            var handler = CreateHandler(handlerType);
            handler.Handle((dynamic)request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public TResponse Send<TResponse>(IRequest<TResponse> request)
    {
        Type handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        try
        {
            var handler = CreateHandler(handlerType);
            return handler.Handle((dynamic)request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    private dynamic CreateHandler(Type handlerType)
    {
        if (_handlerImplementations.TryGetValue(handlerType, out Type implementationType))
        {
            return Activator.CreateInstance(implementationType);
        }

        throw new NotImplementedException($"Handler not found for {handlerType}");
    }
}