using System.Net.Http;
using System.Threading.Tasks;
using Utilities;

namespace IISHostedService
{
    [ExampleServiceBehavior]
    //[InjectionServiceBehavior(typeof(CalculatorServiceInstanceProvider))]
    public class CalculatorService : ICalculator
    {
        private readonly HttpClient _client;

        public CalculatorService()
        {

        }

        //public CalculatorService(HttpClient client)
        //{
        //    _client = client;
        //}

        public Task<int> AddAsync(int a, int b)
        {
            return Task.FromResult(a + b);
        }

        public Task<Result> GenericMathAsync(Operation ops)
        {
            return Task.FromResult(new Result {  Value = ops.A + ops.B });
        }
    }
}
