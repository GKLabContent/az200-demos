using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using POCOs;

namespace Models
{
    public class SchoolContext : DbContext
    {
        //This is not actually used.  Storage table is used.
        private string connectionString = "Your connection string";
        public SchoolContext() : base() { }
        public SchoolContext(string connection) : base()
        {

        }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

        }



    }
}
