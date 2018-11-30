using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AZ200T03._1_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace AZ200T03._1_WebApp.Controllers
{
    [Route("api/School")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        //TODO: T03.1 -Storage account Connection string
        private string connectionString = "";
        private CloudStorageAccount account;

        public SchoolController():base()
        {
            account = CloudStorageAccount.Parse(connectionString);
        }

 

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("students");
            await table.CreateIfNotExistsAsync();
            var query = new TableQuery<Student>();
            var students = await table.ExecuteQuerySegmentedAsync(query, null);
            return students.ToList();
            
        }

        [HttpPost]
        public async Task AddStudent(Student data)
        {
            var client = account.CreateCloudQueueClient();
            var queue = client.GetQueueReference("newstudents");
            await queue.CreateIfNotExistsAsync();
            await queue.AddMessageAsync(new CloudQueueMessage(data.Name));
           
        }
    }
}