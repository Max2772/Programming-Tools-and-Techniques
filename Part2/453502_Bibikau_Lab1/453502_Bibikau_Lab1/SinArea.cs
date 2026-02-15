using System.Diagnostics;

class SinArea
{
    public delegate void FinishHandler(double result, long ticks);
    public delegate void ProgressHandler(double progress);
    
    public event FinishHandler? OnFinish;
    public event ProgressHandler? OnProgress;
    
    public const double STEP = 0.00000001;
    public const double LATENCY = 10;
    
    private Semaphore semaphore;
    
    public SinArea(int maxCount = 8)
    {
        semaphore = new Semaphore(maxCount, maxCount);
    }
    
    public double Evaluate(double l, double r)
    {
        semaphore.WaitOne();
        
        Stopwatch sw = new Stopwatch();
        sw.Start();
        double res = 0;
        for (double x = l; x <= r; x += STEP)
        {
            res += Math.Sin(x + STEP / 2) * STEP;
            for (int i = 0; i < LATENCY; i++)
            {
                double temp = 6.0 * 7.0;
            }
            OnProgress?.Invoke((x - l) / (r - l));
        }
        sw.Stop();
        OnProgress?.Invoke(1);
        OnFinish?.Invoke(res, sw.Elapsed.Ticks);
        semaphore.Release();
        return res;
    }
}