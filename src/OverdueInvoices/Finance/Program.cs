﻿using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Finance
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceName = typeof(Program).Namespace;
            Console.Title = serviceName;

            var config = new EndpointConfiguration(serviceName);
            config.UseTransport<LearningTransport>();
            config.UsePersistence<LearningPersistence>();
            config.SendFailedMessagesTo("error");

            var endpointInstance = await Endpoint.Start(config);

            Console.WriteLine($"{serviceName} started. Press any key to stop.");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}
