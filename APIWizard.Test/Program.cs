using APIWizard.Builders;
using APIWizard.Test;

var apiClient = new APIClientBuilder()
    .WithConfigurationFile("schema.json")
    .Build();

var forecast = await apiClient.DoRequestAsync<Forecast>("forecastct", null, CancellationToken.None);

Console.WriteLine(forecast);