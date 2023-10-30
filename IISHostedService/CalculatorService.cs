using System;
using System.Threading.Tasks;

namespace IISHostedService
{
    public class CalculatorService : ICalculatorService
    {
        public Task<int> AddAsync(int A, int B)
        {
            return Task.FromResult(A + B);
        }

        public async Task<Result> GenericMathAsync(Operation op)
        {
            return new Result { Value = op.A + op.B };
        }
    }
}
