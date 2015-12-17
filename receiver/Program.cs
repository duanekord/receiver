using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;

namespace receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubConnectionString = "Endpoint=sb://pendletoneventhub-ns.servicebus.windows.net/;SharedAccessKeyName=ReceiveRule;SharedAccessKey=+UB+Fak8rj/JLet3mhOe/fbM1B8tYhAvSN6wwqUW32s=";
            string eventHubName = "pendletoneventhub";
            string storageAccountName = "duanekord";
            string storageAccountKey = "XlLxQZ80siskOjCvd0p5X17oRB3hyiQ28T+cGF1/HlZvYggnQR0qCFDfEPpqd+cVV48MLcYznaKpQn1X6n+YGw==";
            string storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageAccountName, storageAccountKey);

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            Console.WriteLine("Registering EventProcessor...");
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>().Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
