using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;



namespace AZ200T02._3_5
{
    class Program
    {
        //TODO: T02.3-5 - Set your storage account connection string
        private static string connectionString = "Your storage account connection string";
        private static CloudStorageAccount account;
        static void Main(string[] args)
        {
            account = CloudStorageAccount.Parse(connectionString);
            fileShareDemo().GetAwaiter();
            blobDemo().GetAwaiter();
            Console.WriteLine("Finished Main");
            Console.ReadLine();
        }

        private static async Task fileShareDemo()
        {
            var client = account.CreateCloudFileClient();
            var share = client.GetShareReference("demoshare");
            await share.CreateIfNotExistsAsync();


            CloudFileDirectory rootDir = share.GetRootDirectoryReference();
            CloudFileDirectory appDir = rootDir.GetDirectoryReference("Application_Logs");
            await appDir.CreateIfNotExistsAsync();

            Console.WriteLine("File Share:\tCreated share and folder");

            CloudFile file = appDir.GetFileReference("log.json");
            await file.UploadTextAsync($"{{'app':'{AppDomain.CurrentDomain.FriendlyName}', 'date':'{DateTime.Now}'");
            Console.WriteLine("File Share:\tUploaded file");


            await share.FetchAttributesAsync();
            share.Properties.Quota = 24;
            await share.SetPropertiesAsync();
            Console.WriteLine("File Share:\tSet quota");



        }

        private static async Task blobDemo()
        {
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("democontainer");
            if(await container.CreateIfNotExistsAsync())
            {
                container.Metadata["project"] = "AZ200T02.5";
                await container.SetMetadataAsync();
                Console.WriteLine("Blob:\tCreated container and set metadata.");

            }

            CloudBlockBlob blob = container.GetBlockBlobReference("demo.txt");
            await blob.UploadTextAsync("This is a sample file");
            Console.WriteLine("Blob:\tUploaded file.");

            string sas = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy { Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write, SharedAccessExpiryTime = DateTime.UtcNow.AddHours(12) });

            Console.WriteLine($"Blob:\tSAS - {sas}");


        }
    }
}
