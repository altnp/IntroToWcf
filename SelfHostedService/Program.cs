using System;

namespace SelfHostedService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GeopointServiceHosting.Run();
            Console.ReadKey();
        }
    }
}
