using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace AZ200T02._2
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine("Hello World!");
        }
        private static async Task cosmosDBDemo()
        {
            var databaseName = "sqlDB";
            var collectionName = "people";
            var endpoint = "";
            var key = "";

            DocumentClient client = new DocumentClient(new Uri(endpoint), key);


            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);


            var document = new { firstName = "Alex", lastName = "Leh" };
            await client.CreateDocumentAsync(collectionUri, document);



            var query = client.CreateDocumentQuery<Family>(collectionUri, new SqlQuerySpec() { QueryText = "SELECT * FROM f WHERE (f.surname = @lastName)", Parameters = new SqlParameterCollection() { new SqlParameter("@lastName", "Andt") } }, DefaultOptions); var families = query.ToList();

        }
    }
    class Family
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
