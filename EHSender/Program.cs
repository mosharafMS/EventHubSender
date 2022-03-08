using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using System.Text;
using System.Diagnostics;
using Azure.Messaging.EventHubs.Producer;

namespace EHSender
{
    class Program
    {
        private static IConfiguration config;
        
        private static EventHubProducerClient eventHubClient;
        public static async Task Main(string[] args)
        {
            initializeConfigurations();
            string eventHubName = config["eventHubName"];
            string connectionString = config["EHConnectionString"];
            int latency_ms = 0;
            int.TryParse(config["latencyMS"], out latency_ms);

            Console.WriteLine("Start sending to eventHub {0}", eventHubName);

            EHconnect(eventHubName, connectionString);

            Console.WriteLine("press CTRL+C to stop sending");

            long counter = 0;
            Console.WriteLine();
            
            while (true)
            { 
                await sendEHMessage("deviceID");
                counter += 1;
                int curpos = Console.CursorTop;
                Console.SetCursorPosition(0, curpos);
                Console.Write("{0} messages sent",counter);
                if(latency_ms>0)
                {
                    System.Threading.Thread.Sleep(latency_ms);
                }
            }
        }

        private static void EHconnect(string EventHubName, string EventHubConnectionString)
        {
            eventHubClient = new EventHubProducerClient(EventHubConnectionString, EventHubName);
        }
        private static async Task sendEHMessage(string partitionKey)
        {
            
            DataGenerator generator = new DataGenerator();
            var message = generator.generateDateJson();
            using EventDataBatch eventBatch = await eventHubClient.CreateBatchAsync();
            var eventData = new EventData(Encoding.UTF8.GetBytes(message));
            eventBatch.TryAdd(eventData);
            await eventHubClient.SendAsync(eventBatch);
        }

        private static void initializeConfigurations()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("configurations.json")
                .Build();
        }
    }
}
