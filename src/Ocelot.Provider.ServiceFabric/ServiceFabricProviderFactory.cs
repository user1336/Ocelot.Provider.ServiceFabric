﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Client;
using Ocelot.Configuration;
using Ocelot.Logging;
using Ocelot.ServiceDiscovery;

namespace Ocelot.Provider.ServiceFabric
{
    public class ServiceFabricProviderFactory
    {
        public static ServiceDiscoveryFinderDelegate Get = (provider, config, reRoute) =>
        {
            var factory = provider.GetService<IOcelotLoggerFactory>();
            return GetServiceFabricProvider(provider, config, reRoute, factory);
        };

        private static ServiceDiscovery.Providers.IServiceDiscoveryProvider GetServiceFabricProvider(IServiceProvider provider, ServiceProviderConfiguration config, DownstreamReRoute reRoute, IOcelotLoggerFactory factory)
        {
            var servicePartitionResolver = provider.GetService<IServicePartitionResolver>();
            var serviceFabricDiscoveryProvider = new ServiceFabricProvider(reRoute.ServiceName, servicePartitionResolver, factory);
            return serviceFabricDiscoveryProvider;
        }

        // Support Ocelot <= 13.5.2
        private static ServiceDiscovery.Providers.IServiceDiscoveryProvider GetServiceFabricProvider(IServiceProvider provider, ServiceProviderConfiguration config, string reRoute, IOcelotLoggerFactory factory)
        {
            var servicePartitionResolver = provider.GetService<IServicePartitionResolver>();
            var serviceFabricDiscoveryProvider = new ServiceFabricProvider(reRoute, servicePartitionResolver, factory);
            return serviceFabricDiscoveryProvider;
        }
    }
}
