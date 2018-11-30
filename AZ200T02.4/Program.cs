using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AZ200T02._4
{
    class Program
    {
        static void Main(string[] args)
        {
            var school = new SchoolContext();
            if (school.Database.EnsureCreated())
            {
                school.Students.Add(new Student { Name = "Al" });
                school.Students.Add(new Student { Name = "Barb" });
                school.Students.Add(new Student { Name = "Cal" });
                school.Students.Add(new Student { Name = "Donna" });
                school.SaveChanges();
            }

            Console.WriteLine("All Students");
            foreach(var student in school.Students)
            {
                Console.WriteLine(student.ToString());
            }

            Console.WriteLine("Selected Student");
            string calInfo = getCal(school).GetAwaiter().GetResult();
            Console.WriteLine(calInfo);

            Console.WriteLine("Done");
            Console.ReadLine();
        }
        static async Task<string> getCal(SchoolContext school)
        {
            var cal = await school.Students.FirstAsync(s => s.Name == "Cal");
            return cal.ToString();
        }
    }

    
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime RegisteredOn { get; set; }
        public override string ToString()
        {
            return $"\tID:\t{ID}\n\tName:\t{Name}";
        }
    }

    public class SchoolContext:DbContext
    {
        //TODO: T02.4 - Add Azure SQL database connection string
        private string connectionString = "Your Azure SQL database connection string";
        public SchoolContext() : base() { }
        public SchoolContext(string connection) : base()
        {
            
        }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

        }

 

    }


}
