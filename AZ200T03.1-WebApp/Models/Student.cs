using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AZ200T03._1_WebApp.Models
{
    public class Student:TableEntity
    {
        public Student()
        {
            PartitionKey = "Student";
        }
        public int ID { get =>  int.Parse(RowKey); set => RowKey=value.ToString(); }
        public string Name { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
