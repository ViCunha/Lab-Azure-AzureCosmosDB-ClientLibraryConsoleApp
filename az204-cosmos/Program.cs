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


    
