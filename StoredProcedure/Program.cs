using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;

namespace StoredProcedure
{
    class Program
    {
        static string CosmosConnection = "";
        static string CosmosDatabase = "CosmosClientServiceDatabase";
        static string CosmosContainer = "CosmosServiceContainer";
        static void Main(string[] args)
        {
            CosmosClient cosmos = new CosmosClient(CosmosConnection, new CosmosClientOptions { AllowBulkExecution = true});
            Container container = cosmos.GetContainer(CosmosDatabase,CosmosContainer);

            dynamic[] array = new dynamic[]
            {
                new { id = "2", UniqueId = "123", name = "", city = "Kolkata"}
            };

            //call stored proc
            string count = container.Scripts.ExecuteStoredProcedureAsync<string>("addproc", new PartitionKey("123"), new[] { array }).GetAwaiter().GetResult();

            //call trigger
            //container.CreateItemAsync<AddDetails>(item, null, new ItemRequestOptions { PreTriggers = new List<string> { "AddData" } });

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
