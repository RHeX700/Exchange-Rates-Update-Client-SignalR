using System.Threading;
using System.Threading.Tasks;
using Exchange_Rates_Update_Client;

Console.WriteLine("Hello, World!");

var alpha = new AlphaVintageClient("USD", "JPY");

var gamma = new ChartUpdateClient("", alpha);

Timer timer = new Timer(callback: new TimerCallback(gamma.UpdateChart), state: null, dueTime: 1000, period: 15000);
