using Microsoft.AspNetCore.SignalR.Client;

namespace Exchange_Rates_Update_Client;

public partial class ChartUpdateClient
{
    HubConnection connection;
    public AlphaVintageClient alphaVintageClient { get; set; }
    public ChartUpdateClient(string hubUrl, AlphaVintageClient alphaVintageClient){
        connection = new HubConnectionBuilder().WithUrl(hubUrl).Build();
        this.alphaVintageClient = alphaVintageClient;

        connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0,5) * 1000);
                await connection.StartAsync();
            };
    }

    public Task ValueReceiver(string chartValue)
    {        
        return default;
    }

    public void UpdateChart(object? state)
    {
        try
        {
            var result = alphaVintageClient.Query();
            connection.SendAsync("ValueSender", result).RunSynchronously();

        }catch{
            Console.WriteLine("An error occured in the Chart Update Client");
        }

    }

    public async Task ChangeCurrencyPair(string FromCurrency, string ToCurrency)
    {
        alphaVintageClient.fromCurrency = FromCurrency;
        alphaVintageClient.toCurrency = ToCurrency;
    }

    public Task CurrencyPairPageUpdate()
    {
        throw new NotImplementedException();
    }
}