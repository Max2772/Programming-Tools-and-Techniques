namespace LB4;

public partial class LoadingPage : ContentPage
{
    public LoadingPage()
    {
        InitializeComponent();
    }
    
    bool restartCalculation = false;
    private CancellationTokenSource Token;
    private Task<double> FindInt;
    
    private async void StartClicked(object sender, EventArgs e)
    {
        restartCalculation = FindInt != null && !FindInt.IsCompleted;
        
        Token?.Cancel();
        Token = new CancellationTokenSource();

        try
        {
            Label.Text = restartCalculation ? "Перезапуск расчета" : "Вычисление";

            FindInt = Task.Run(() => GetIntegral(Token.Token));

            double result = await FindInt;

            Label.Text = $"Результат вычислений: {Math.Round(result, 4)}";
            ProgressBar.Progress = 1;
            ProgressLabel.Text = "100 %";
        }
        catch (OperationCanceledException)
        {
            if (!restartCalculation)
                Label.Text = "Задача отменена";
        }
    }
    
    private void CancelClicked(object sender, EventArgs e)
    {
        restartCalculation = false;
        Token?.Cancel();
    }
    
    private async Task<double> GetIntegral(CancellationToken Token)
    {
        //double step = 0.00000001;
        double step = 0.00005;
        double latency = 100000;
        double ans = 0;
        double lastPercent = -1;

        for (double i = 0; i <= 1; i += step)
        {
            Token.ThrowIfCancellationRequested();

            ans += Math.Sin(i) * step;
            
            for (int j = 0; j < latency; j++)
            {
                double a = 2 * 2;
            }

            int percent = (int)(i * 100);
            
            if (percent != lastPercent)
            {
                lastPercent = percent;

                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressBar.Progress = percent / 100.0;
                    ProgressLabel.Text = $"{percent} %";
                });

                await Task.Yield();
            }
        }

        return ans;
    }
}