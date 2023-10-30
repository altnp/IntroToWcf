using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Utilities;

namespace IISHostedService
{
    public class CalculatorServiceInstanceProvider : InjectionInstanceProvider
    {
        public CalculatorServiceInstanceProvider(Type serviceType) 
            : base(serviceType)
        {
        }

        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CalculatorService>();
            serviceCollection.AddTransient<HttpClient>();
        }
    }
}