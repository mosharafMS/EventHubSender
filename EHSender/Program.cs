using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Diagnostics;

namespace EHSender
{
    class Program
    {
        private static IConfiguration config;
        private static EventHubClient eventHubClient;
        public static async Task Main(string[] args)
        {
            initializeConfigurations();
            string eventHubName = config["eventHubName"];
            string connectionString = config["EHConnectionString"];

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
            }
        }

        private static void EHconnect(string EventHubName, string EventHubConnectionString)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EventHubConnectionString)
            {
                EntityPath = EventHubName
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
        private static async Task sendEHMessage(string partitionKey)
        {
            
            DataGenerator generator = new DataGenerator();
            var message = generator.generateDateJson();
            await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)), partitionKey);
        }

        private static void initializeConfigurations()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("configurations.json")
                .Build();
        }
    }
}
