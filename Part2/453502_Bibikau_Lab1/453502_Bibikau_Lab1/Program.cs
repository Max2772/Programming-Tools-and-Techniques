const int THROTTLE_COUNT = 10000000 - 1;

static void AttachSinAreaLoggers(SinArea calc)
{
    calc.OnFinish += (res, time) =>
    {
        Console.WriteLine($"Поток {Environment.CurrentManagedThreadId}: Завершен с результатом: {res} (время: {time} ticks)");
    };
    long throttler = 0;
    calc.OnProgress += (progress) =>
    {
        if (Interlocked.Read(ref throttler) % THROTTLE_COUNT == 0 || progress == 1)
        {
            string bar = new string('=', (int)Math.Floor(progress * 32));
            if (progress != 1)
            {
                bar = (bar + ">").PadRight(32);
            }
            Console.WriteLine($"Поток {Environment.CurrentManagedThreadId}: [{bar}] {Math.Floor(progress * 100)}%");
        }
        Interlocked.Increment(ref throttler);
    };
}

static void PrintHeader(string title)
{
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("=======================================");
    Console.WriteLine(title);
    Console.WriteLine("");
}


PrintHeader("0: Запуск вычисления в отдельном потоке (один поток)");
SinArea calc0 = new SinArea();
AttachSinAreaLoggers(calc0);
Thread t0 = new Thread(() => calc0.Evaluate(0, 1));
Console.WriteLine($"Создан поток {t0.ManagedThreadId}");
t0.Start();
t0.Join();

PrintHeader("1: Запуск двух экземпляров в разных потоках с приоритетами");
SinArea calc1 = new SinArea();
AttachSinAreaLoggers(calc1);
Thread t1 = new Thread(() => calc1.Evaluate(0, 1));
t1.Priority = ThreadPriority.Highest;
Console.WriteLine($"Создан high priority поток {t1.ManagedThreadId}");
Thread t2 = new Thread(() => calc1.Evaluate(0, 1));
t2.Priority = ThreadPriority.Lowest;
Console.WriteLine($"Создан low priority поток {t2.ManagedThreadId}");
t1.Start();
t2.Start();
t1.Join();
t2.Join();

PrintHeader("2: Только один поток может выполняться одновременно (5 потоков запущено)");
SinArea calc2 = new SinArea(1);
AttachSinAreaLoggers(calc2);
List<Thread> threads2 = new List<Thread>(5);
for (int i = 0; i < 5; i++)
{
    threads2.Add(new Thread(() => calc2.Evaluate(0, 1)));
}
foreach (Thread t in threads2)
{
    t.Start();
}
foreach (Thread t in threads2)
{
    t.Join();
}

PrintHeader("3: Только три потока могут выполняться одновременно (5 потоков запущено)");
SinArea calc3 = new SinArea(3);
AttachSinAreaLoggers(calc3);
List<Thread> threads3 = new List<Thread>(5);
for (int i = 0; i < 5; i++)
{
    threads3.Add(new Thread(() => calc3.Evaluate(0, 1)));
}
foreach (Thread t in threads3)
{
    t.Start();
}
foreach (Thread t in threads3)
{
    t.Join();
}

