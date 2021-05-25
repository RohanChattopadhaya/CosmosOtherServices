using Microsoft.Azure.Cosmos.Table;
using System;

namespace CosmosTableClientOperations
{
    class Program
    {
        private readonly static string connectionString = "";
        private readonly static string tableName = "EmployeeTable";
        static void Main(string[] args)
        {
            Insert();
            //ReadData();
            //Delete();
            Console.ReadKey();
        }

        private static void Insert()
        {
            try
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);

                CloudTableClient client = account.CreateCloudTableClient();

                CloudTable table = client.GetTableReference(tableName);

                table.CreateIfNotExists();

                EmployeeDetails employeeDetails = new EmployeeDetails("em003", "Rh");
                employeeDetails.City = "Bangalore";
                employeeDetails.Name = "So";
                employeeDetails.State = "WestBengal";

                TableOperation tableOperation = TableOperation.Insert(employeeDetails);
                //TableOperation tableOperation = TableOperation.InsertOrMerge(employeeDetails);
                //TableOperation tableOperation = TableOperation.InsertOrReplace(employeeDetails);

                TableResult result = table.Execute(tableOperation);

                if (result.HttpStatusCode.Equals(204))
                {
                    Console.WriteLine("Inserted Data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message : " + ex.Message);
                Console.WriteLine("Message : " + ex.StackTrace);
            }
        }

        private static void ReadData()
        {
            try
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);

                CloudTableClient client = account.CreateCloudTableClient();

                CloudTable table = client.GetTableReference(tableName);

                table.CreateIfNotExists();

                EmployeeDetails employeeDetails = new EmployeeDetails("em003", "Rh");

                TableOperation tableOperation = TableOperation.Retrieve<EmployeeDetails>(employeeDetails.PartitionKey, employeeDetails.RowKey);

                TableResult result = table.Execute(tableOperation);

                if (result.HttpStatusCode.Equals(200))
                {
                    EmployeeDetails employee = result.Result as EmployeeDetails;
                    Console.WriteLine("Name :" + employee.Name);
                    Console.WriteLine("City :" + employee.City);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Message : " + ex.Message);
                Console.WriteLine("Message : " + ex.StackTrace);
            }
        }

        private static void Delete()
        {
            try
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);

                CloudTableClient client = account.CreateCloudTableClient();

                CloudTable table = client.GetTableReference(tableName);

                table.CreateIfNotExists();

                EmployeeDetails employeeDetails = new EmployeeDetails("em002", "Ri");

                TableOperation tableOperation = TableOperation.Retrieve<EmployeeDetails>(employeeDetails.PartitionKey, employeeDetails.RowKey);

                TableResult resultRead = table.Execute(tableOperation);

                if (resultRead.HttpStatusCode.Equals(200))
                {
                    EmployeeDetails employee = resultRead.Result as EmployeeDetails;

                    TableOperation tableDeleteOperation = TableOperation.Delete(employee);

                    TableResult result = table.Execute(tableOperation);


                    if (result.HttpStatusCode.Equals(200))
                    {
                        Console.WriteLine("Deleted");
                    }
                }
                else
                {
                    Console.WriteLine("Some Problem Ocuured");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Message : " + ex.Message);
                Console.WriteLine("Message : " + ex.StackTrace);
            }
        }
    }
}
