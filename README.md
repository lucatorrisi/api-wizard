<p align="center"><img src="https://raw.githubusercontent.com/lucatorrisi/api-wizard/master/logo.png"></p>
<p align="center">
<img src="https://img.shields.io/appveyor/build/lucatorrisi/api-wizard"><img src="https://img.shields.io/nuget/v/APIWizard"><img src="https://img.shields.io/github/issues/lucatorrisi/api-wizard"><img src="https://img.shields.io/github/license/lucatorrisi/api-wizard">
</p>

## Introduction

APIWizard is a .NET 7 NuGet package that simplifies the process of creating a customized HTTP client for making HTTP calls to third-party services. It allows you to configure the client through various sources, making API integration easier and more flexible.

## Features

- **Customized HTTP Client**: APIWizard helps you build a specialized HTTP client tailored to the specific API requirements.

- **Configuration Options**: You can configure the client using local JSON definitions, IConfigurationSection, or even remote JSON OpenAPI configurations.

- **Support for Multiple API Versions**: APIWizard supports OpenAPI V2 and V3, providing compatibility with various APIs.

- **Dependency Injection Ready**: APIWizard is designed to be easily integrated into ASP.NET Core applications using Dependency Injection.

## Installation

APIWizard can be installed using the NuGet package manager or the .NET CLI:

```shell
dotnet add package APIWizard
```
## Get started
#### Client creation
With a Local JSON Definition
```csharp
var apiClient = new APIClientBuilder()
    .WithOpenAPIConfigurationFile("schema.json")
    .WithOpenAPIVersion(OpenAPIVersion.V2)
    .Build();
```
With an IConfigurationSection
```csharp
var apiClient = new APIClientBuilder()
    .WithOpenAPIConfiguration(builder.Configuration.GetSection("APIWizard"))
    .WithOpenAPIVersion(OpenAPIVersion.V2)
    .WithClientOptions(options =>
    {
        // Specify technical options
        options.PooledConnectionLifetime = TimeSpan.FromMinutes(2);
    })
    .Build();
```
With Remote JSON OpenAPI Configuration
```csharp
var apiClient = new APIClientBuilder()
    .WithOpenAPIUrlConfiguration("https://raw.githubusercontent.com/OAI/OpenAPI-Specification/main/examples/v3.0/petstore-expanded.json")
    .WithOpenAPIVersion(OpenAPIVersion.V3)
    .Build();
```
#### Dependency Injection for ASP.NET Core Apps
```csharp
builder.Services.AddAPIWizardClient(apiClient);
```

#### Usage
```csharp
var sampleResponse = await apiClient.DoRequestAsync("/store/inventory", CancellationToken.None);
```

#### Configuration Example (or URL to an OpenAPI json)
```json
{
  "host": "petstore.swagger.io",
  "basePath": "/v2",
  "schemes": [
    "https",
    "http"
  ],
  "paths": {
    "/store/inventory": {
      "get": {
        "tags": [
          "store"
        ],
        "summary": "Returns pet inventories by status",
        "description": "Returns a map of status codes to quantities",
        "operationId": "getInventory",
        "produces": [
          "application/json"
        ],
        "parameters": [],
        "responses": {
          "200": {
            "description": "successful operation",
            "schema": {
              "type": "object",
              "additionalProperties": {
                "type": "integer",
                "format": "int32"
              }
            }
          }
        },
        "security": [
          {
            "api_key": []
          }
        ]
      }
    }
  }
}
```
#### License
APIWizard is licensed under the Apache 2.0 License. See the LICENSE file for details.
