using System.Globalization;

namespace LB3;

public partial class CalculatorPage : ContentPage
{
    string input = "";
    
    double? accumulator = null;

    string pendingOperator = null;

    private static readonly CultureInfo CI = CultureInfo.InvariantCulture;
    
    public CalculatorPage()
    {
        InitializeComponent();
    }

    private void PressButton(object sender, EventArgs e)
    {
        if (!(sender is Button button))
            return;

        string t = button.Text;

        if (t == "C")
        {
            if (input.Length > 0)
            {
                input = input.Remove(input.Length - 1);
                DisplayLabel.Text =
                    input.Length > 0 ? input : (accumulator?.ToString(CI) ?? "0");
            }
            else if (pendingOperator != null)
            {
                pendingOperator = null;
                DisplayLabel.Text = accumulator?.ToString(CI) ?? "0";
            }
            else
            {
                accumulator = null;
                DisplayLabel.Text = "0";
            }

            return;
        }
        
        if (t == "10^x")
        {
            double value;
            if (input.Length == 0 && accumulator.HasValue)
                value = accumulator.Value;
            else if (!double.TryParse(input, NumberStyles.Any, CI, out value))
            {
                DisplayLabel.Text = "Error";
                input = "";
                return;
            }

            try
            {
                double res = Math.Pow(10, value);
                accumulator = res;
                input = "";
                pendingOperator = null;
                DisplayLabel.Text = res.ToString(CI);
            }
            catch
            {
                DisplayLabel.Text = "Error";
                input = "";
                accumulator = null;
                pendingOperator = null;
            }

            return;
        }
        
        if (t == ",")
        {
            if (!input.Contains("."))
            {
                if (input.Length == 0)
                    input = "0.";
                else
                    input += ".";
                DisplayLabel.Text = input;
            }

            return;
        }
        
        if (t == "+" || t == "-" || t == "*" || t == "/")
        {
            if (input.Length > 0)
            {
                if (!double.TryParse(input, NumberStyles.Any, CI, out double val))
                {
                    DisplayLabel.Text = "Error";
                    input = "";
                    return;
                }

                if (accumulator == null)
                    accumulator = val;
                else
                {
                    if (!ApplyOperation(ref accumulator, val, pendingOperator))
                    {
                        DisplayLabel.Text = "Error";
                        input = "";
                        accumulator = null;
                        pendingOperator = null;
                        return;
                    }
                }
            }
            else
            {
                if (accumulator == null)
                    accumulator = 0;
            }

            pendingOperator = t;
            input = "";
            DisplayLabel.Text = accumulator?.ToString(CI) ?? "0";
            return;
        }

        if (t == "=")
        {
            if (pendingOperator != null && input.Length > 0)
            {
                if (!double.TryParse(input, NumberStyles.Any, CI, out double val))
                {
                    DisplayLabel.Text = "Error";
                    input = "";
                    return;
                }

                if (!ApplyOperation(ref accumulator, val, pendingOperator))
                {
                    DisplayLabel.Text = "Error";
                    input = "";
                    accumulator = null;
                    pendingOperator = null;
                    return;
                }

                DisplayLabel.Text = accumulator?.ToString(CI) ?? "0";
                input = accumulator?.ToString(CI) ?? "";
                pendingOperator = null;
            }
            else
            {
                DisplayLabel.Text =
                    input.Length > 0 ? input : (accumulator?.ToString(CI) ?? "0");
            }

            return;
        }

        input += t;
        DisplayLabel.Text = input;
    }

    private bool ApplyOperation(ref double? acc, double newValue, string op)
    {
        if (acc == null)
        {
            acc = newValue;
            return true;
        }

        try
        {
            switch (op)
            {
                case "+":
                    acc = acc.Value + newValue;
                    break;
                case "-":
                    acc = acc.Value - newValue;
                    break;
                case "*":
                    acc = acc.Value * newValue;
                    break;
                case "/":
                    if (newValue == 0.0)
                        return false;
                    acc = acc.Value / newValue;
                    break;
                default:
                    acc = newValue;
                    break;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}