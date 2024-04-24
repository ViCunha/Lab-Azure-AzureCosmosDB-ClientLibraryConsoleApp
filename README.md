### Overview
---
Exercise to create a console app to perform the following operations in Azure Cosmos DB:

- Connect to an Azure Cosmos DB account
- Create a database
- Create a container
- Create an item 
- Read an item
- Query an item

### Key Aspects
---
- To be defined 

### Environment
---
Microsoft Azure Portal
- Valid subscription
- Azure Cloud Shell

Integrated Development Environment (IDE)
- Visual Studio Community 2022 or Visual Studio Code (with C# extension)

Framework
- .NET 6 or greater

### Actions
---

Prepare the environment

- Create the project folder

- Open the Visual Studio Code using the project folder as the base

- Open Azure Cloud Shell (https://portal.azure.com/#cloudshell/)

- Create a resource group for the resources needed for this exercise

```
az account list-locations --output table
CURRENTMOMENT="20240423172000"
LOCATION="eastus2"
RESOURCEGROUP="myresourcegroup${CURRENTMOMENT}"
az group create --name ${RESOURCEGROUP} --location ${LOCATION}
```
- Create Azure Cosmos DB Account
```
COSMOSDBNAME="mycosmosdbaccount${CURRENTMOMENT}"
az cosmosdb create --resource-group ${RESOURCEGROUP} --name ${COSMOSDBNAME}
```
- Retrieve the primary key for the Azure Cosmos DB account
```
az cosmosdb keys list --resource-group ${RESOURCEGROUP} --name ${COSMOSDBNAME}
```

Configure and code the console application

- Create a folder for the project and change it to the folder
```
md az204-cosmos
cd az204-cosmos
```

- Create the .NET console app
```
dotnet new console
```

- Add packages using statements
```
dotnet add package Microsoft.Azure.Cosmos
```

- The Program.cs must have the code below
```
using System;
using Microsoft.Azure.Cosmos;

public class Program
{
    const string ENDPOINTURL = "https://mycosmosdbaccount20240423172000.documents.azure.com:443/";

    private static readonly string PRIMARYKEY01 = "PRIMARY-KEY-01";

    public static async Task Main(string[] args)
    {
        var CosmosClientOrchestration = new CosmosClientOrchestration(ENDPOINTURL, PRIMARYKEY01);
        await CosmosClientOrchestration.Run();
    }
}

public class CosmosClientOrchestration
{

    private CosmosClient _cosmosClient;

    private Database _database;

    private Container _container;

    private string databaseId = "az204Database";

    private string containerId = "az204Container";

    private string _endPointURL;
    
    private string _primaryKey01;

    public CosmosClientOrchestration(string endPointURL, string primaryKey01)
    {
        this._endPointURL = endPointURL;
        this._primaryKey01 = primaryKey01;
    }

    public async Task Run()
    {
        try
        {
            Console.WriteLine(">> Begin ");
            
            //
            this._cosmosClient = GetCosmosDbClinet();

            //
            this._database = await CreateDatabaseAsync();

            //
            this._container = await CreateContainerAsync();
            
        }

        catch (CosmosException ex)
        {
            Console.WriteLine("{0} - {1}", ex.StatusCode, ex.Message);
            throw;
        }

        catch (Exception ex)
        {
            Console.WriteLine("{0}", ex.Message);
            throw ex;
        }

        finally
        {
            Console.WriteLine(">> End");
            Console.ReadLine();
        }

    }

    private async Task<Container> CreateContainerAsync()
    {
        Console.WriteLine("- Create Container: Begining");
        var result = await this._database.CreateContainerAsync(containerId, "/date" ) ;
        Console.WriteLine("- Create Container: End");
        return null;
    }

    private async Task<Database> CreateDatabaseAsync()
    {
        Console.WriteLine("- Create Database: Begining");
        var result = await this._cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("- Create Database: End");
        return result;
    }

  
    private CosmosClient GetCosmosDbClinet()
    {
        Console.WriteLine("- Create a Connection: Begining");
        var result = new CosmosClient(this._endPointURL, this._primaryKey01);
        Console.WriteLine("- Create a Connection: End");        
        return result;
    }

}
```

Publish the code

- Create the repository Lab-Azure-AzureCosmosDB-ClientLibraryConsoleApp

- Generate a new specific/dedicated personal access token (Fine-grained tokens)
```
20240426-CosmosDB-SDKConsoleApp
```

- Publish the code to GitHub
```
git init

git config --global user.name "Vinicius Cunha"
git config --global user.email "vicunha@outlook.com"

git remote add origin https://github.com/ViCunha/Lab-Azure-AzureCosmosDB-ClientLibraryConsoleApp.git

git remote set-url origin https://ViCunha:ACCESSKEY01@github.com/ViCunha/Lab-Azure-AzureCosmosDB-ClientLibraryConsoleApp.git
// htts://ViCunha:([ACCESS-TOKEN])@github.com/[USER]/[REPOSITORY]

git fetch origin
git switch main
git pull origin

git add .
git commit -m "message"

git push origin
```

Clean up resources

- In the “search resources, services, and doc”, type and select Resource Groups

- Click on the resource group created

- Select Delete resource group and follow the directions to delete the resource group and all of the resources it contains

### Media
---
![image](https://github.com/ViCunha/Lab-Azure-AzureCosmosDB-ClientLibraryConsoleApp/assets/65992033/b6a58897-3ac1-4902-b9fd-63acebbe982a)
---
![image-20240424-164340](https://github.com/ViCunha/Lab-Azure-AzureCosmosDB-ClientLibraryConsoleApp/assets/65992033/362f5e98-8ef3-48ad-b453-4509b5dbe580)

### References
---
- [Exercise: Create resources by using the Microsoft .NET SDK v3](https://learn.microsoft.com/en-us/training/modules/work-with-cosmos-db/3-exercise-work-cosmos-db-dotnet)
