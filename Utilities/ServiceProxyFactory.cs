using System;
using System.Linq;
using System.ServiceModel;
#if !NETFRAMEWORK
using System.ServiceModel.Channels;
using Microsoft.Extensions.Configuration;
#endif

namespace Tcetra.ServiceClient
{
    public class ServiceProxyFactory
    {
#if !NETFRAMEWORK
        protected static readonly object ConfigLock = new object();
        protected static IConfiguration Configuration;

        public static void RegisterConfiguration(IConfiguration configuration)
        {
            lock (ConfigLock)
            {
                Configuration = configuration;
            }
        }
#endif
    }

    public class ServiceProxyFactory<T> : ServiceProxyFactory
        where T : class
    {
        internal static readonly object InitLock = new object();
        internal static readonly object AbortLock = new object();

        internal static ChannelFactory<T> ChannelFactory = null;

        public static T CreateChannel()
        {
            InitializeChannelFactory();

            T channel;
            try
            {
                channel = ChannelFactory.CreateChannel();
            }
            catch (Exception ex)
            {
                lock (AbortLock)
                {
                    if (ChannelFactory != null)
                    {
                        try
                        {
                            ChannelFactory.Abort();
                        }
                        catch (Exception)
                        { }
                        ChannelFactory = null;
                    }
                }

                throw new Exception("Exception creating channel from ChannelFactory.", ex);
            }

            return channel;
        }

        private static void InitializeChannelFactory()
        {
            if (ChannelFactory != null)
                return;

            lock (InitLock)
            {
                if (ChannelFactory != null)
                    return;
#if NETFRAMEWORK
                ChannelFactory = new ChannelFactory<T>("*");

#else
                var configSection = $"WCFServices:{typeof(T).Name}";
                var endpointUrl = Configuration[$"{configSection}:Endpoint"];

                if (string.IsNullOrEmpty(endpointUrl))
                    throw new ArgumentNullException($"{configSection}:Endpoint");

                var section = Configuration.GetSection($"{configSection}:Binding");
                var protocol = endpointUrl?.Split(":").FirstOrDefault();
                Binding binding = protocol switch
                {
                    "http" => section.Get<BasicHttpBinding>() ?? new BasicHttpBinding { MaxReceivedMessageSize = int.MaxValue },
                    "https" => section.Get<BasicHttpsBinding>() ?? new BasicHttpsBinding { MaxReceivedMessageSize = int.MaxValue },
                    "net.tcp" => section.Get<NetTcpBinding>() ?? new NetTcpBinding(SecurityMode.None) { MaxReceivedMessageSize = int.MaxValue },
                    _ => throw new NotSupportedException($"Unsupported Protocol '{protocol}'")
                };

                var endpointAddress = new EndpointAddress(endpointUrl);
                ChannelFactory = new ChannelFactory<T>(binding, endpointAddress);
#endif
            }
        }
    }
}
