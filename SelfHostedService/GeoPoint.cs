namespace SelfHostedService
{
    public class GeoPoint
    {
        public decimal Lat { get; set; }
        public decimal Long { get; set; }

        public GeoPoint()
        {
               
        }

        public GeoPoint(decimal lat, decimal @long)
        {
            Lat = lat;
            Long = @long;
        }
    }
}
