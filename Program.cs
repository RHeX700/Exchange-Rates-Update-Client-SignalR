using System.Threading;
using Exchange_Rates_Update_Client;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var alpha = new AlphaVintageClient("USD", "AUD");

        Console.WriteLine("Kindly supply the Hub's Url");

        string HubUrl = Console.ReadLine();


        ChartUpdateClient gamma = new ChartUpdateClient(HubUrl, alpha);

        Timer timer = new Timer(callback: gamma.UpdateChart, state: null, dueTime: 0, period: 15000);
        Console.WriteLine("Press Enter to exit the program.");
        Console.ReadLine();
        timer.Dispose();
    }
}