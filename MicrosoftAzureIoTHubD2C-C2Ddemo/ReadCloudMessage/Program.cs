using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadCloudMessage
{
    class Program
    {
        static string connectionString = "HostName=logeshIoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=v8S3el/ZNsD8FjuKFiuuRoMzmYK3cwMP7lDSn8L5Mv4=";
        static string iotHubEndPoint = "messages/events";
        static EventHubClient eventhubclient;
        static void Main(string[] args)
        {

            Console.WriteLine("Cloud Read Messages From Console");

            eventhubclient = EventHubClient.CreateFromConnectionString(connectionString, iotHubEndPoint);

            var d2cPartition = eventhubclient.GetRuntimeInformation().PartitionIds;
            CancellationTokenSource cts = new CancellationTokenSource();
            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("Exiting");
            };
            var tasks = new List<Task>();
            foreach (var partition in d2cPartition)
            {
                tasks.Add(ReceiveMessageFromClodAsync(partition, cts.Token));
            }
            Task.WaitAll(tasks.ToArray());
        }
        private static async Task ReceiveMessageFromClodAsync(string partition, CancellationToken ct)
        {
            var eventReceiver = eventhubclient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);

            while (true)
            {
                if (ct.IsCancellationRequested) break;
                EventData eventData = await eventReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine(data);

            }
        }
    }
}
