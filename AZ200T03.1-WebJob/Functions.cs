using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace AZ200T03._1_WebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("newstudents")] string message, TextWriter log)
        {
            var connection = ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString;
            var account = CloudStorageAccount.Parse(connection);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("students");
            var insert = TableOperation.Insert(new Student { ID = message.GetHashCode(), Name = message, RegisteredOn = DateTime.UtcNow });
            table.Execute(insert);
            log.WriteLine(message);
        }
    }
    public class Student : TableEntity
    {
        public Student()
        {
            PartitionKey = "Student";
        }
        public int ID { get => int.Parse(RowKey); set => RowKey = value.ToString(); }
        public string Name { get; set; }
        public DateTime RegisteredOn { get; set; }
    }

}
