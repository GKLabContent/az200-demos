using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AZ200T02._1
{
    class Program
    {
        //TODO: T02.1 - Set your storage connection info
        private static string connectionString = "Your storage account connection string";
        private static string accountName = "Your storage account";
        private static string accountKey = "Your storage account key";

        static void Main(string[] args)
        {
            tableSDK().GetAwaiter();
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static async Task tableSDK()
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("demo");
            await table.CreateIfNotExistsAsync();
            string[] cities = { "Cary", "Tokyo", "London" };
            //Insert some rows
            for(int i = 0; i < 100; i++)
            {
                var person = new PersonEntity { PartitionKey = cities[i % 3], City = cities[i % 3], RowKey = i.ToString(), Name = $"Person {i}" };
                var insert = TableOperation.Insert(person);
                await table.ExecuteAsync(insert);
            }

            //Retrieve some rows
            var query = new TableQuery<PersonEntity>() { FilterString = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Cary") };
            var results = await table.ExecuteQuerySegmentedAsync<PersonEntity>(query, null);
            foreach(var result in results)
            {
                Console.WriteLine($"SDK record # {result.RowKey} - Name: {result.Name}");
            }
        }
    }

    class PersonEntity:TableEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}
