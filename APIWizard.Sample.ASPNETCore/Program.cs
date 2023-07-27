using APIWizard.Builders;
using APIWizard.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var apiClient = new APIClientBuilder()
//    .WithConfiguration(builder.Configuration.GetSection("APIWizard"))
//    .Build();

var apiClient = new APIClientBuilder()
    .WithSwaggerUrlConfiguration("https://petstore.swagger.io/v2/swagger.json")
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
