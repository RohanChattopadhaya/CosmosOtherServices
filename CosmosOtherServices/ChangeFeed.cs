using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CosmosOtherServices
{
    public static class ChangeFeed
    {
        [FunctionName("ChangeFeed")]
        public static void Run([CosmosDBTrigger(
            databaseName: "CosmosClientServiceDatabase",
            collectionName: "CosmosServiceContainer",
            ConnectionStringSetting = "CosmosConnection",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            //if (input != null && input.Count > 0)
            //{
            //    log.LogInformation("Documents modified " + input.Count);
            //    log.LogInformation("First document Id " + input[0].Id);
            //}

            foreach (var item in input)
            {
                Console.WriteLine("Data ID : " + item.Id);
                Console.WriteLine("Data UniqueName : " + item.GetPropertyValue<string>("UniqueName"));
                Console.WriteLine("Data EmailID :  " + item.GetPropertyValue<string>("EmailID"));
            }
        }
    }
}
