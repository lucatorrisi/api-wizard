![Logo](https://raw.githubusercontent.com/lucatorrisi/api-wizard/6b057ad1aea91e594eeeeb53d2e1ce2cbfbf95f4/logo.svg)

[![License: Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
![Build Status](https://img.shields.io/travis/your-username/APIWizard)
![Nuget](https://img.shields.io/nuget/v/APIWizard)

**APIWizard is a .NET 7 NuGet package that simplifies the process of creating a customized HTTP client for making HTTP calls to third-party services. It allows you to configure the client through a JSON configuration file.**

## Installation

APIWizard can be installed using the NuGet package manager or the .NET CLI:

```shell
dotnet add package APIWizard
```
## Usage
### Console Application
```csharp
var apiClient = new APIClientBuilder()
    .WithConfigurationFile("schema.json")
    .Build();

var forecast = await apiClient.DoRequestAsync<Forecast>("forecast", null, CancellationToken.None);
```
### ASP Net Application DI
```csharp
var apiClient = new APIClientBuilder()
    .WithConfiguration(Configuration.GetSection("APIWizard:Schema"))
    .Build();

services.AddAPIWizard(apiClient);
```

### Configuration Example
```json
{
  "host": "api.open-meteo.com",
  "basePath": "/v1",
  "schemes": [ "http", "https" ],
  "paths": [
    {
      "name": "forecast",
      "route": "forecast?latitude=52.52&longitude=13.41",
      "method": "get",
      "consumes": "application/json",
      "security": {
        "type": "APIKey",
        "key": "xyz"
      }
    }
  ]
}

```