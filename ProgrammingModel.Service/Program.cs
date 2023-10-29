using System;
using System.ServiceModel;

namespace ProgrammingModel.Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(NicksPetStore), new Uri("http://localhost:8081/NicksPetStore"));
            host.Open();

            Console.ReadLine();
        }
    }
}
