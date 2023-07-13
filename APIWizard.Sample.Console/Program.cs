using APIWizard.Builders;
using APIWizard.Sample.Common;

var apiClient = new APIClientBuilder()
    .WithConfigurationFile("schema.json")
    .Build();

var forecast = await apiClient.DoRequestAsync<Forecast>("forecast", null, CancellationToken.None);

Console.WriteLine(forecast);