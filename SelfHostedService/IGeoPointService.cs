using System.ServiceModel;
using System.Threading.Tasks;

namespace SelfHostedService
{
    [ServiceContract]
    internal interface IGeoPointService 
    {
        [OperationContract]
        Task<GeoPoint> GetGeoPointAsync(Address address);
    }
}
