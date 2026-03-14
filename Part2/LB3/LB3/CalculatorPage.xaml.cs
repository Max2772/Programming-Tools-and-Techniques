using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB3;

public partial class CalculatorPage : ContentPage
{
    String input="";
    public CalculatorPage()
    {
        InitializeComponent();
    }

    private void PressButton(object sender, EventArgs e)
    {
        if (sender is Button button)
        {

            if(button.Text=="exp(x)")
            {
                double res = 0;
                bool no_exc = true;
                if(double.TryParse(input,out res))
                {
                    try
                    {
                        res=Math.Exp(res);
                    }
                    catch(Exception ex)
                    {
                        no_exc = false;
                    }

                    if (no_exc)
                    {
                        input="";
                        DisplayLabel.Text = res.ToString();
                        return;
                    }
                      
                }

                DisplayLabel.Text = "Error";
                input = "";
                return;
            }

            if(button.Text== "C")
            {
                if(input.Length>0)
                    input = input.Remove(input.Length - 1);
            }
            else
                input += button.Text;

            DisplayLabel.Text = input;
            
        }
    }
}
