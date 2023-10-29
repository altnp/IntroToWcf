using ProgrammingModel.Client;
using System.ServiceModel;

var binding = new BasicHttpBinding();
var endpointAddress = new EndpointAddress("http://localhost:8081/NicksPetStore");

using var client = new PetStoreClient(binding, endpointAddress);
await client.ListPetsAsync();

var channelFactory = new ChannelFactory<IPetStore>(binding, endpointAddress);
var channel = channelFactory.CreateChannel();//technically a disposable
await channel.SellPetAsync(new Pet("Milo", Species.Dog));

((IDisposable)channel).Dispose();
