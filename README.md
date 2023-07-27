![Logo](https://raw.githubusercontent.com/lucatorrisi/api-wizard/6b057ad1aea91e594eeeeb53d2e1ce2cbfbf95f4/logo.svg)

[![License: Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
![Build Status](https://img.shields.io/travis/your-username/APIWizard)
![Nuget](https://img.shields.io/nuget/v/APIWizard)

**APIWizard is a .NET 7 NuGet package that simplifies the process of creating a customized HTTP client for making HTTP calls to third-party services. It allows you to configure the client through various sources.**

## Installation

APIWizard can be installed using the NuGet package manager or the .NET CLI:

```shell
dotnet add package APIWizard
```
## Get started
### Client creation
```csharp
// With local JSON definition OR
var apiClient = new APIClientBuilder()
    .WithConfigurationFile("schema.json")
    .Build();
// With an IConfigurationSection OR
var apiClient = new APIClientBuilder()
    .WithConfigurationFile(builder.Configuration.GetSection("APIWizard"))
    .Build();
// With remote JSON swagger configuration
var apiClient = new APIClientBuilder()
    .WithSwaggerUrlConfiguration("https://petstore.swagger.io/v2/swagger.json")
    .Build();
```
### Usage
```csharp
// DI for ASP.NET Core Apps OR
builder.Services.AddAPIWizardClient(apiClient);
// Direct use for Console App
var sampleResponse = await apiClient.DoRequestAsync<Inventory>("/store/inventory", null, CancellationToken.None);
```

### Configuration Example (or URL to a Swagger json)
```json
{
    "host": "api.open-meteo.com",
    "basePath": "/v1",
    "schemes": [
      "https",
      "http"
    ],
    "paths": {
      "forecast?latitude=52.52&longitude=13.41": {
        "get": {
          "consumes": [
            "multipart/form-data"
          ],
          "produces": [
            "application/json"
          ]
        }
      }
    }
  }

```