using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace SelfHostedService
{
    public static class GeoPointServiceHosting
    {
        public static void Run()
        {
            var host = new ServiceHost(typeof(GeoPointService));
            host.ConfigureServiceEndpoint<IGeoPointService>();
            host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
            host.ConfigureMetaDataEndpoint();
            host.Open();
        }

        private static void ConfigureServiceEndpoint<T>(this ServiceHost host)
        {
            var contract = ContractDescription.GetContract(typeof(T));
            var binding = new NetTcpBinding
            {
                PortSharingEnabled = true,
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None,
                    Transport = new TcpTransportSecurity
                    {
                        ProtectionLevel = ProtectionLevel.None
                    }
                },
                MaxReceivedMessageSize = 1024 * 1024
            };

            var endpoint = new EndpointAddress($"net.tcp://localhost:55777/{typeof(T).Name}");
            host.AddServiceEndpoint(new ServiceEndpoint(contract, binding, endpoint));
        }

        private static void ConfigureMetaDataEndpoint(this ServiceHost host)
        {
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());

            var binding = new CustomBinding(MetadataExchangeBindings.CreateMexTcpBinding());
            binding.Elements.Find<TcpTransportBindingElement>().PortSharingEnabled = true;

            host.AddServiceEndpoint(typeof(IMetadataExchange), binding, $"net.tcp://localhost:55777/{host.Description.ServiceType.Name}");
        }
    }
}
