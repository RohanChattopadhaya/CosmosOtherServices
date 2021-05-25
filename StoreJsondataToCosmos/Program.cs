using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StoreJsondataToCosmos
{
    class Program
    {
        static string connectionString = "";
        static string databaseName = "JsonDatabase";
        static string containerName = "JsonContainer";
        static async Task Main(string[] args)
        {
            CosmosClient client = new CosmosClient(connectionString);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/Operationname");

            FileStream fs = new FileStream(System.Environment.CurrentDirectory + @"\Details\QueryResult.json",FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            JsonTextReader jsonTextReader = new JsonTextReader(sr);

            Container container = client.GetContainer(databaseName, containerName);

            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType == JsonToken.StartObject)
                {
                    JObject jObject = JObject.Load(jsonTextReader);
                    Datas datas = jObject.ToObject<Datas>();
                    datas.id = Guid.NewGuid().ToString();
                    await container.CreateItemAsync<Datas>(datas, new PartitionKey(datas.Operationname));
                    Console.WriteLine("Data" + datas.Correlationid);
                }
            }
            Console.WriteLine("All Added");
            
        }
    }
}
