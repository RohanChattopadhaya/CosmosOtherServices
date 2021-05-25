using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlobTriggerToCosmos
{
    public static class BlobToCosmos
    {
        [FunctionName("BlobToCosmos")]
        public static void Run([BlobTrigger("data/{name}", Connection = "BlobConnection")]Stream myBlob,
            [CosmosDB(
               databaseName:"CosmosClientServiceDatabase",
               collectionName:"CosmosServiceContainer",
               ConnectionStringSetting="CosmosConnection"
            )] out dynamic document,
            string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            BlobData blob = new BlobData
            {
                Name = name,
                size = myBlob.Length
            };

            document = blob;
        }
    }
}
