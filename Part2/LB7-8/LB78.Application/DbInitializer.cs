using Microsoft.Extensions.DependencyInjection;

namespace LB78.Application;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        await unitOfWork.DeleteDataBaseAsync();
        await unitOfWork.CreateDataBaseAsync();
        var random = new Random();

        for (int i = 1; i <= 3; i++)
        {
            await unitOfWork.SushiSetRepository.AddAsync(new SushiSet
            {
                Id = i,
                Name = $"Набор {i}",
                Price = random.Next(15, 60),
                PhotoPath = string.Empty,
                SushiList = []
            });
        }

        await unitOfWork.SaveAllAsync();

        for (int i = 1; i <= 3; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                await unitOfWork.SushiRepository.AddAsync(new Sushi(
                    $"Суши {(j + 1) + (i - 1) * 10}",
                    random.Next(0, 10),
                    random.Next(30, 120),
                    "Рис, нори, начинка",
                    i)
                {
                    Id = (i - 1) * 10 + (j + 1),
                });
            }
        }

        await unitOfWork.SaveAllAsync();
    }
}
