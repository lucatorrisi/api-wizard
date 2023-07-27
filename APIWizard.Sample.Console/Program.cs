using APIWizard.Builders;
using APIWizard.Sample.Common;

//var apiClient = new APIClientBuilder()
//    .WithConfigurationFile("schema.json")
//    .Build();
var apiClient = new APIClientBuilder()
    .WithSwaggerUrlConfiguration("https://petstore.swagger.io/v2/swagger.json")
    .Build();

var sampleResponse = await apiClient.DoRequestAsync<Inventory>("/store/inventory", null, CancellationToken.None);

Console.WriteLine(sampleResponse);