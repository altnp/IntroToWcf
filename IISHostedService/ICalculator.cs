using System.ServiceModel;
using System.Threading.Tasks;

namespace IISHostedService
{
    [ServiceContract]
    public interface ICalculator
    {

        [OperationContract]
        Task<int> AddAsync(int a, int b);

        [OperationContract]
        Task<Result> GenericMathAsync(Operation ops);
    }

    public class Operation
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class Result
    {
        public int Value { get; set; }
    }
}
