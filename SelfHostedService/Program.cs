using System;

namespace SelfHostedService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GeoPointServiceHosting.Run();

            Console.ReadKey();
        }
    }
}
