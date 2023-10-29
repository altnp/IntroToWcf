using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ProgrammingModel.Client
{
    [ServiceContract]
    internal interface IPetStore
    {
        [OperationContract]
        Task<List<Pet>> ListPetsAsync(Species? species = null);

        [OperationContract]
        Task SellPetAsync(Pet pet);
    }

    internal enum Species { Cat, Dog, Hamster }

    [DataContract]
    internal class Pet
    {
        public string Name { get; set; }
        public Species Species { get; set; }

        public Pet(string name, Species species)
        {
            Name = name;
            Species = species;
        }
    }

    internal class PetStoreClient : ClientBase<IPetStore>, IPetStore
    {
        public PetStoreClient(Binding binding, EndpointAddress endpointAddress) :
            base(binding, endpointAddress)
        {

        }

        public Task<List<Pet>> ListPetsAsync(Species? species = null)
        {
            return Channel.ListPetsAsync(species);
        }

        public Task SellPetAsync(Pet pet)
        {
            return Channel.SellPetAsync(pet);
        }
    }
}
