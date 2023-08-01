using APIWizard.Builders;
using APIWizard.Enums;
using APIWizard.Sample.Common;

// GET
var apiClient = new APIClientBuilder()
    .WithConfigurationFile("schema.json")
    .WithVersion(OpenAPIVersion.V2)
    .WithOptions(options =>
    {
        options.PooledConnectionLifetime = TimeSpan.FromMinutes(2);
    })
    .Build();

var sampleResponse1 = await apiClient.SendRequestAsync<Inventory>("/store/inventory", cancellationToken: CancellationToken.None);

// POST with form data
FileStream fileStreamRead = new FileStream("logo.svg", FileMode.Open, FileAccess.Read);
var dictionary = new Dictionary<object, object>
{
    {"logo.svg", fileStreamRead }
};

var sampleResponse2 = await apiClient.SendRequestAsync<PetUploadImage>("/pet/1/uploadImage", "post", dictionary, cancellationToken: CancellationToken.None);

Console.WriteLine(sampleResponse2.Message);
