using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableClientOperations
{
    public class EmployeeDetails: TableEntity
    {
        public EmployeeDetails()
        {

        }
        public EmployeeDetails(string code,string uniqueName)
        {
            PartitionKey = code;
            RowKey = uniqueName;
        }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
