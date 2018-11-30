using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace AZ200T03_5
{
    public static class QueueDemoFunction
    {
        [FunctionName("QueueDemoFunction")]
        public static void Run([QueueTrigger("my-queue-items", Connection = "QueueStorage")]string myQueueItem, [Queue("my-queue-items-out", Connection = "QueueStorage")] out string copy, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
            copy = myQueueItem;
        }
    }
}
