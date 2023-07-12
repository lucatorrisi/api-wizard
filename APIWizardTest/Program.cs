using APIWizard.Builders;

var apiClient = new APIClientBuilder()
    .WithConfigurationFile("schema.json")
    .Build();

Console.WriteLine("END");