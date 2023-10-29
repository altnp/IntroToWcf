#if NETFRAMEWORK
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Utilities
{
    public abstract class InjectionInstanceProvider : IInstanceProvider
    {
        private readonly Type _serviceType;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<InstanceContext, IServiceScope> _scopes;

        public InjectionInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _scopes = new ConcurrentDictionary<InstanceContext, IServiceScope>();
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            var scope = _serviceProvider.CreateScope();
            _scopes.TryAdd(instanceContext, scope);
            return scope.ServiceProvider.GetRequiredService(_serviceType);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            _scopes.TryRemove(instanceContext, out var scope);
            scope.Dispose();
        }

        public abstract void ConfigureServices(IServiceCollection serviceCollection);
    }

    public class InjectionServiceBehavior : Attribute, IServiceBehavior
    {
        public Type _injectionInstanceProviderType;
        public InjectionServiceBehavior(Type injectionInstanceProviderType)
        {
            _injectionInstanceProviderType = injectionInstanceProviderType;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var cdb in serviceHostBase.ChannelDispatchers)
            {
                if (cdb is ChannelDispatcher cd)
                {
                    foreach (var ed in cd.Endpoints)
                    {
                        ed.DispatchRuntime.InstanceProvider = (InjectionInstanceProvider)Activator.CreateInstance(_injectionInstanceProviderType,
                            new object[] { serviceDescription.ServiceType });
                    }
                }
            }
        }
    }
}
#endif