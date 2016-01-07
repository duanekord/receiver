using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
using Amqp;

namespace receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubConnectionString =
                "Endpoint=sb://Enter Connection String";
            string eventHubName = "Enter Event Hub Name";
            string storageAccountName = "Enter Storage Account Name";
            string storageAccountKey =
                "Enter Storage Account Key";
            string storageConnectionString =
                string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName,
                    storageAccountKey);

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName,
                EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            Console.WriteLine("Registering EventProcessor...");
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>().Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }

    }
}

