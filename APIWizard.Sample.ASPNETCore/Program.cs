using APIWizard.Builders;
using APIWizard.Enums;
using APIWizard.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var apiClient = new APIClientBuilder()
    .WithOpenAPIUrlConfiguration("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/main/examples/v3.0/petstore-expanded.json")
    .WithVersion(OpenAPIVersion.V3)
    .WithOptions(options =>
    {
        options.PooledConnectionLifetime = TimeSpan.FromMinutes(2);
    })
    .Build();

builder.Services.AddAPIWizardClient(apiClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
