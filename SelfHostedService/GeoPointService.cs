using System.Threading.Tasks;

namespace SelfHostedService
{
    internal class GeoPointService : IGeoPointService
    {
        public Task<GeoPoint> GetGeoPointAsync(Address address)
        {
            return Task.FromResult(new GeoPoint(1.0m, 1.0m));
        }
    }
}
