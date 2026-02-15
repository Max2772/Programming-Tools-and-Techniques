const int THROTTLE_COUNT = 10000000 - 1;

void attachSinAreaLoggers(SinArea calc) {
    calc.OnFinish += (res, time) =>
    {
        Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} finished in {time} ticks. Result: {res}");
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
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} [{bar}] {Math.Floor(progress * 100)}%");
        }
        Interlocked.Increment(ref throttler);
    };
}

void printHeader(string title) {
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("=======================================");
    Console.WriteLine(title);
    Console.WriteLine("");
}

{
    printHeader("0: Starting computation in main thread");
    SinArea calc = new SinArea();
    attachSinAreaLoggers(calc);
    calc.Evaluate(-2, 5);
}

{
    printHeader("1: Starting computation in two threads with different priorities");
    SinArea calc = new SinArea();
    attachSinAreaLoggers(calc);

    Thread t1 = new(() => calc.Evaluate(-2, 5));
    t1.Priority = ThreadPriority.Highest;
    Console.WriteLine($"Created high priority thread {t1.ManagedThreadId}");
	Thread t2 = new(() => calc.Evaluate(-2, 5));
	t2.Priority = ThreadPriority.Lowest;
    Console.WriteLine($"Created low priority thread {t2.ManagedThreadId}");
    t1.Start();
	t2.Start();

	t1.Join();
	t2.Join();
}


{
    printHeader("2: Only one can remain");
    SinArea calc = new SinArea(1);
    attachSinAreaLoggers(calc);

    List<Thread> threads = new(5);
    for(int i = 0; i < 5; i++) {
        threads.Add(new Thread(() => calc.Evaluate(-2, 5)));
    }

    foreach(Thread t in threads) {
        t.Start();
    }

    foreach(Thread t in threads) {
        t.Join();
    }
}


{
    printHeader("3: Well actually three can remain");
    SinArea calc = new SinArea(3);
    attachSinAreaLoggers(calc);

    List<Thread> threads = new(5);
    for(int i = 0; i < 5; i++) {
        threads.Add(new Thread(() => calc.Evaluate(-2, 5)));
    }

    foreach(Thread t in threads) {
        t.Start();
    }

    foreach(Thread t in threads) {
        t.Join();
    }
}