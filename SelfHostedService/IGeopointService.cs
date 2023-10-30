using System.ServiceModel;
using System.Threading.Tasks;

namespace SelfHostedService
{
    [ServiceContract]
    public interface IGeopointService
    {
        [OperationContract]

        Task<Geopoint> GetGeopointAsync(Address address);
    }

    public class Geopoint
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class GeopointService : IGeopointService
    {
        public Task<Geopoint> GetGeopointAsync(Address address)
        {
            return Task.FromResult(new Geopoint { Lat = 1.0, Long = 1.0 });
        }
    }
}
