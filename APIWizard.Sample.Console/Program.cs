using APIWizard.Builders;
using APIWizard.Enums;
using APIWizard.Sample.Common;

var apiClient = new APIClientBuilder()
    .WithConfigurationFile("schema.json")
    .WithVersion(OpenAPIVersion.V2)
    .WithOptions(options =>
    {
        options.PooledConnectionLifetime = TimeSpan.FromMinutes(2);
    })
    .Build();

var sampleResponse = await apiClient.SendRequestAsync<Inventory>("/store/inventory", cancellationToken: CancellationToken.None);

Console.WriteLine(sampleResponse);