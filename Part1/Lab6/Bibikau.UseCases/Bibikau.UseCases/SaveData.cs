using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Bibikau.Domain;
using Bibikau.Mediator;

namespace Bibikau.UseCases;

/// <summary>
/// Запрос на сохранение коллекции в файл
/// </summary>
/// <param name="Data">Коллекция объектов</param>
/// <param name="FileName">имя файла</param>
public sealed record SaveData(IEnumerable<SushiSet> Data, string FileName) : IRequest;

/// <summary>
/// Обработчик запроса
/// </summary>
public class SaveDataHandler : IRequestHandler<SaveData>
{
    public void Handle(SaveData request)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(request.Data, options);
        File.WriteAllText(request.FileName, json);
    }
}

