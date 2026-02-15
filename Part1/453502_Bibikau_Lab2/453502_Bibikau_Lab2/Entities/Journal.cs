using System.Numerics;

namespace _453502_Bibikau_Lab2.Entities;

public class Journal<T> where T: INumber<T>
{
    public struct EventLogger
    {
        public string Description;
        public string EntityName;
    };

    private MyCustomCollection<EventLogger> eventLoggers = new();

    public void TariffAdd(Tariff<T> tariff)
    {
        EventLogger eventLogger;
        eventLogger.EntityName = "Услуга";
        eventLogger.Description = $"{tariff.Name} подключена!";

        LogEvent(eventLogger);
    }

    public void TariffRemove(Tariff<T> tariff)
    {
        EventLogger eventLogger;
        eventLogger.EntityName = "Услуга";
        eventLogger.Description = $"{tariff.Name} отключена!";

        LogEvent(eventLogger);
    }

    public void UserAdd(User user)
    {
        EventLogger eventLogger;
        eventLogger.EntityName = "Пользователь";
        eventLogger.Description = $"{user.Name} зарегистрирован!";

        LogEvent(eventLogger);
    }

    public void UserRemove(User user)
    {
        EventLogger eventLogger;
        eventLogger.EntityName = "Пользователь";
        eventLogger.Description = $"{user.Name} отключен!";

        LogEvent(eventLogger);
    }
    
    public void ServiceUsageAdd(ServiceUsage<T> serviceUsage)
    {
        EventLogger eventLogger;
        eventLogger.EntityName = "Новая услуга";
        eventLogger.Description = $"{serviceUsage.Tariff.Name} подключена пользователю {serviceUsage.User.Name}!";

        LogEvent(eventLogger);
    }
    
    public void LogEvent(EventLogger eventLogger)
    {
        eventLoggers.Add(eventLogger);
    }

    public void ShowEvents()
    {
        foreach (var eventLogger in eventLoggers)
        {

            Console.WriteLine(eventLogger.EntityName + " " + eventLogger.Description);

        }
    }
}