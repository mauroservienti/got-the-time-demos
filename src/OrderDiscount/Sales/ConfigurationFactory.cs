﻿using System;
using NServiceBus;

namespace Sales
{
    class ConfigurationFactory
    {
        public EndpointConfiguration CreateConfiguration()
        {
            var config = new EndpointConfiguration("Sales");

            config.UseSerialization<SystemJsonSerializer>();
            config.UseTransport<LearningTransport>();
            config.UsePersistence<LearningPersistence>();
            config.SendFailedMessagesTo("error");

            return config;
        }
    }
}