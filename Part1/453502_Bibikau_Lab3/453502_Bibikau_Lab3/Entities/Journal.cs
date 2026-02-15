using _453502_Bibikau_Lab3;

namespace _453502_Bibikau_Lab3;

public class Journal
{
    public struct EventLogger
    {
        public string Description;
        public string EntityName;
    };

    private List<EventLogger> eventLoggers = new();

    public void TariffAdd(Tariff tariff)
    {
        EventLogger eventLogger;
        eventLogger.EntityName = "Услуга";
        eventLogger.Description = $"{tariff.Name} подключена!";

        LogEvent(eventLogger);
    }

    public void TariffRemove(Tariff tariff)
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
    
    public void ServiceUsageAdd(ServiceUsage serviceUsage)
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