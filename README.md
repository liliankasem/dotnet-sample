# Dotnet Isolated SDK Binding Sample

## Prerequisites

- .NET
  - [v6](https://dotnet.microsoft.com/download/dotnet/6.0) or [v7](https://dotnet.microsoft.com/download/dotnet/7.0)
- Host Version: 4.15+
  - v4.16 required for collection binding support
- Core Tools
- Dotnet Isolated Worker Packages
  - Microsoft.Azure.Functions.Worker: 1.18.0
  - Microsoft.Azure.Functions.Worker.Sdk: 1.12.0
- Blob Storage
  - [Emulator](https://learn.microsoft.com/azure/storage/common/storage-use-azurite?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json&bc=%2Fazure%2Fstorage%2Fblobs%2Fbreadcrumb%2Ftoc.json&tabs=visual-studio)
    - `"AzureWebJobsStorage": "UseDevelopmentStorage=true"`
  - Portal
    - Create a new Blob Storage account in the Azure Portal
    - Update `"AzureWebJobsStorage"` value in `local.settings.json` with your
      Blob Storage connection string.
- CosmosDB
  - [Emulator](https://learn.microsoft.com/azure/cosmos-db/local-emulator?tabs=ssl-netstd21)
    - `"CosmosDBConnection": "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="`
  - Portal
    - Create a new CosmosDB resource in the Azure Portal
    - Update `CosmosDBConnection` value in `local.settings.json` with your
        CosmosDB connection string.

## Running this sample project (local)

- `dotnet build`
- `func start`
  - Make sure you're using the core tools build installed in the prerequisite steps
