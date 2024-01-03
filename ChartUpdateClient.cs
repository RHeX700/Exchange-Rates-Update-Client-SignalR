using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace Exchange_Rates_Update_Client;

public partial class ChartUpdateClient
{
    HubConnection connection;
    public AlphaVintageClient alphaVintageClient { get; set; }
    public ChartUpdateClient(string hubUrl, AlphaVintageClient alphaVintageClient){
        connection = new HubConnectionBuilder().WithUrl(hubUrl).Build();
        this.alphaVintageClient = alphaVintageClient;
        connection.StartAsync().Wait();
    }

    public Task ValueReceiver(string chartValue)
    {        
        return default;
    }

    public void UpdateChart(object? state)
    {

        Console.WriteLine("Attempting Query...");
        var result = alphaVintageClient.Query();
        result.Wait();
        Console.WriteLine(result.Result);
        connection.SendAsync("ValueSender", result.Result).Wait();
    }

    public async Task ChangeCurrencyPair(string FromCurrency, string ToCurrency)
    {
        alphaVintageClient.fromCurrency = FromCurrency;
        alphaVintageClient.toCurrency = ToCurrency;
        connection.SendAsync("CurrencyPairPageUpdate", FromCurrency, ToCurrency).Wait();
    }

    public Task CurrencyPairPageUpdate()
    {
        throw new NotImplementedException();
    }
}