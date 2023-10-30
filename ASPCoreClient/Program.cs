using GeoPointService;
using System.ServiceModel;
using Tcetra.ServiceClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IGeoPointService>((sp) =>
{
    var address = builder.Configuration["GeoPointServiceAddress"];
    return new GeoPointServiceClient(new NetTcpBinding(SecurityMode.None), new EndpointAddress(address));
});

//builder.Services.AddTransient((sp) =>
//{
//    var address = builder.Configuration["GeoPointServiceAddress"];
//    return new ChannelFactory<IGeoPointService>(new NetTcpBinding(SecurityMode.None), new EndpointAddress(address)).CreateChannel();
//});

//ServiceProxyFactory.RegisterConfiguration(builder.Configuration);
//builder.Services.AddTransient(sp => ServiceProxyFactory<IGeoPointService>.CreateChannel());

var app = builder.Build();
app.MapGet("/", (IGeoPointService geoPointService) => geoPointService.GetGeoPointAsync(new Address()));
app.Run();
