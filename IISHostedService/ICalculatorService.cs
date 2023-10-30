using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace IISHostedService
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        Task<int> AddAsync(int A, int B);
        [OperationContract]
        Task<Result> GenericMathAsync(Operation op);
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
