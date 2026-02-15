using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Bibikau.Domain; 
using Bibikau.Mediator;

namespace Bibikau.UseCases;

/// <summary>
/// Запрос на чтение файла
/// </summary>
/// <param name="FileName">имя файла</param>
public sealed record ReadFile(string FileName) : IRequest<IEnumerable<SushiSet>>;

/// <summary>
/// Обработчик запроса чтения файла
/// </summary>
public class ReadFileHandler : IRequestHandler<ReadFile, IEnumerable<SushiSet>>
{
    public IEnumerable<SushiSet> Handle(ReadFile request)
    {
        if (!File.Exists(request.FileName)) return new List<SushiSet>();
        
        string json = File.ReadAllText(request.FileName);
        return JsonSerializer.Deserialize<IEnumerable<SushiSet>>(json) ?? new List<SushiSet>();
    }
}