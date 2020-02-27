using Microsoft.Extensions.DependencyInjection;
using System;

namespace SalesTaxes
{
    public sealed class ServiceProviderSingleton
    {
        private static ServiceProviderSingleton instance = null;
        private static readonly object padlock = new object();
        private readonly ServiceProvider _serviceProvider;
        
        public ServiceProvider ServiceProvider { get => _serviceProvider; }

        ServiceProviderSingleton()  
        {
             _serviceProvider = new ServiceCollection()
            .AddTransient<ITaxCalculator, TaxCalculator>()
            .BuildServiceProvider();
        }

        public static ServiceProviderSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceProviderSingleton();
                    }
                    return instance;
                }
            }
        }

        public void DisposeServices()
        {
            if(_serviceProvider != null)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
