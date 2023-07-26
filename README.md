# Dotnet Isolated SDK Binding Sample

Here are two basic functions to get you started, one is for a trigger binding and the other
is for an input binding (they both bind to a BlobClient). We have an extensive set of binding
samples in the dotnet worker repo that you can refer to and utilize.

[Full binding samples](https://github.com/Azure/azure-functions-dotnet-worker/tree/main/samples/WorkerBindingSamples)

## Prerequisites

- .NET
  - [v6](https://dotnet.microsoft.com/download/dotnet/6.0) or [v7](https://dotnet.microsoft.com/download/dotnet/7.0)
- Host Version: 4.15+
  - v4.16 required for collection binding support
- Core Tools (prerelease for host v4.16)
  - Windows
    - [x64](https://functionsintegclibuilds.blob.core.windows.net/builds/4/latest/Azure.Functions.Cli.win-x64.zip)
    - [x86](https://functionsintegclibuilds.blob.core.windows.net/builds/4/latest/Azure.Functions.Cli.win-x86.zip)
  - [Linux](https://functionsintegclibuilds.blob.core.windows.net/builds/4/latest/Azure.Functions.Cli.linux-x64.zip)
  - [Mac](https://functionsintegclibuilds.blob.core.windows.net/builds/4/latest/Azure.Functions.Cli.osx-x64.zip)
- Dotnet Isolated Worker Packages
  - Microsoft.Azure.Functions.Worker: 1.12.1-preview1
  - Microsoft.Azure.Functions.Worker.Sdk: 1.9.0-preview1
  - Microsoft.Azure.Functions.Worker.Extensions.Storage: 5.1.0-preview1
  - Microsoft.Azure.Functions.Worker.Extensions.CosmosDB: `pending-release`
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

### Core tools setup

- Download one of the core tool zips in the list above
- Unzip the folder to extract the files

#### Mac

- If you are using a Mac and get a notification that `cannot be opened because the developer cannot be verified`,
  go to System Settings -> Privacy and Security -> Allow the package to process
- To run, use the path of the core tools location: `<path_to_core_tools>/func start`

If the above doesn't work for you and you are still seeing permission issues, you can try these steps:

- Navigate into the downloaded core tools folder
- Run command `chmod 755 func`
  - This makes the `func` file an executable
- You can optionally create an alias for this
  - Open your `.zshrc` or `.bash_profile` file
  - Add `alias testfunc='<path_to_core_tools>/func`
  - Save the file and refresh the source `source .zshrc` or `source .bash_profile

> After creating the alias, you may need to restart your terminal and/or VS Code

#### Windows

- Option 1: Reference the `func` executable in the core tools folder when running func host start

    `C:\Users\test_user\Downloads\Azure.Functions.Cli.win-x64\func host start`

- Option 2: Alias (using Set-Alias in Powershell) the `func` executable to the core tools folder
  - Note that this will impact your existing core tools if you don’t reset it when you are done testing,
    alternatively you can use a different alias name to avoid impact i.e. `testfunc`

    `Set-Alias -Name func -Value C:\Users\test_user\Downloads\Azure.Functions.Cli.win-x64\func`

### Blob storage setup

For these base samples to work, you need to make sure the containers that are used by
the bindings exist.

- For the trigger binding, the container is called `client-trigger`
- For the input binding, the container is called `input-container`
  - This container is expected to contain a file called `sample1.txt` which
    you can find in [/src/data](src/data/sample1.txt)

### CosmosDB setup

For these base samples to work, you need to make sure that you have a container and
database is setup:

- Create a Database called `ToDoItems`
- Create a Container called `Items`
- Create a sample ToDo item in the container e.g.

    ```json
    {
        "id": "1",
        "description": "feed the cat"
    }
    ```

### Cosmos DB NuGet

Until the CosmosDB preview SDK is released, you will need to use the app service MyGet.
Update your global NuGet.Config file to include this:

`<add key="azure_app_service" value="https://www.myget.org/F/azure-appservice/api/v2" />`

> [NuGet config docs](https://learn.microsoft.com/nuget/consume-packages/configuring-nuget-behavior)

## Running this sample project (local)

- `dotnet build`
- `func start`
  - Make sure you're using the core tools build installed in the prerequisite steps

## Bug Bash

For the bug bash, here are some scenarios you can test out, but feel free to go outside this list
and experiment but please document everything!

- [Feedback & comments](https://github.com/Azure/azure-functions-dotnet-worker/discussions/1320)
- Please [create an issue](https://github.com/Azure/azure-functions-dotnet-worker/issues/new) for any bugs

### Limitations

- All of the collection scenarios (i.e. IEnumerable<BlobClient> or string[]) will not work in the portal
  but should work locally with Core Tools. The collection support feature has not been released yet and
  will be in the next host release

### Helpful Documentation (preview links)

- [Blob binding overview](https://review.learn.microsoft.com/azure/azure-functions/functions-bindings-storage-blob?branch=pr-en-us-225452&tabs=in-process%2Cextensionv5%2Cextensionv3&pivots=programming-language-csharp)
- [Blob trigger managed identity](https://review.learn.microsoft.com/azure/azure-functions/functions-bindings-storage-blob-trigger?branch=pr-en-us-225452&tabs=in-process&pivots=programming-language-csharp#identity-based-connections)
- [Blob input managed identity](https://review.learn.microsoft.com/azure/azure-functions/functions-bindings-storage-blob-input?branch=pr-en-us-225452&tabs=in-process&pivots=programming-language-csharp#identity-based-connections)
- [Full set of binding samples](https://github.com/Azure/azure-functions-dotnet-worker/tree/main/samples/WorkerBindingSamples)

### Template

| Scenario | Works locally? | Works in the portal? | Works with core tools? | Exceptions or Log Message | Comments |
| -------- | -------------- | -------------------- | ---------------------- | ------------------------- | -------- |
| Blob trigger with BlobClient binding | Yes | Yes | Yes | No errors, logs expected output | |
| | | | | | |

> **We also have an excel template you can use instead (see bug bash meeting invite).**

#### Trigger Binding

- Blob trigger with BlobClient
- Blob trigger with BlobContainerClient
- Blob trigger with BlobContainerClient
- Blob trigger with string
- Blob trigger with byte[]
- Blob trigger with stream
- Blob trigger with POCO
- Blob trigger with system-managed identity
- Blob trigger with user-managed identity
- Blob trigger with app registration
- Blob trigger with custom 'Connection' name
- Blob trigger with no Connection, uses "Storage"
- Large blob payload (gb)

#### Input Binding

- Blob input with BlobClient
- Blob input with BlobContainerClient
- Blob input with BlobContainerClient
- Blob input with string
- Blob input with byte[]
- Blob input with stream
- Blob input with POCO
- Blob input with IEnumerable<BlobClient>
- Blob input with IEnumerable<Stream>
- Blob input with IEnumerable<string>
- Blob input with IEnumerable<byte[]>
- Blob input with IEnumerable<POCO>
- Blob input with string[]
- Blob input with Stream[]
- Blob input with BlobClient[]
- Blob input with byte[][]
- Blob input with POCO[]
- Blob input with system-managed identity
- Blob input with user-managed identity
- Blob input with app registration
- Blob input with custom 'Connection' name
- Blob input with expression with QueueTrigger
- Large blob payload (gb)

> For `IEnumerable` or array scenarios, you will need to set `IsBatched=true` attribute for the binding

## Troubleshooting

### The 'AzureWebJobsScriptRoot' environment variable value is not defined

If you're seeing this error when you deploy to the portal:

`System.InvalidOperationException: The 'AzureWebJobsScriptRoot' environment variable value is not defined. This is a required environment variable that is automatically set by the Azure Functions runtime.`

We just fixed this issue! Make sure you're on the latest worker version:

- `<PackageReference Include="Microsoft.Azure.Functions.Worker" Version="1.12.1-preview1" />`
