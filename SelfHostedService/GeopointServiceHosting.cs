using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedService
{
    internal static class GeopointServiceHosting
    {
        public static void Run()
        {
            var host = new ServiceHost(typeof(GeopointService));
            host.ConfigureServiceEndpoint<IGeopointService>();
            host.ConfigureMetadataEndpoint();
            host.Open();
        }
        private static void ConfigureServiceEndpoint<T> (this ServiceHost host) 
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
                MaxReceivedMessageSize = int.MaxValue
            };
            var endpoint = new EndpointAddress($"net.tcp://localhost:55777/{typeof (T).Name}");
            host.AddServiceEndpoint(new ServiceEndpoint(contract, binding, endpoint));
        }

        private static void ConfigureMetadataEndpoint (this ServiceHost host)
        {
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            var binding = new CustomBinding(MetadataExchangeBindings.CreateMexTcpBinding());
            binding.Elements.Find<TcpTransportBindingElement>().PortSharingEnabled = true;
            host.AddServiceEndpoint(typeof(IMetadataExchange), binding, $"net.tcp://localhost:55777/{host.Description.ServiceType.Name}");
        }
    }
}
