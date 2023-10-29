using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ProgrammingModel.Service
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

    internal class NicksPetStore : IPetStore
    {
        public Task<List<Pet>> ListPetsAsync(Species? species = null)
        {
            return Task.FromResult(new List<Pet>()
            {
                new Pet("Fluffy", Species.Cat),
                new Pet("Waffle", Species.Dog)
            }
            .Where(p => species == null || p.Species == species)
            .ToList());
        }

        public Task SellPetAsync(Pet pet)
        {
            return Task.CompletedTask;
        }
    }


}
