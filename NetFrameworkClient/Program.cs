using NetFrameworkClient.Calculator;
using System;
using System.Threading.Tasks;

namespace NetFrameworkClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var calculator = new CalculatorClient())
            {
                //calculator.Endpoint.EndpointBehaviors.Add(new ExampleEndpointBehavior());
                Console.WriteLine(calculator.Add(100, 1));
                Console.WriteLine(await calculator.AddAsync(100, 1));
            }
        }
    }
}
