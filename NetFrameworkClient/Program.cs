using NetFrameworkClient.ServiceReference1;
using System;
using System.Threading.Tasks;

namespace NetFrameworkClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var calculatorService = new CalculatorServiceClient())
            {
                var result = await calculatorService.AddAsync(1, 2);
                Console.WriteLine(result);
            }
        }
    }
}
