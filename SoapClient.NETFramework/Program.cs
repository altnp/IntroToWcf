using SoapClient.NETFramework.NumberConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapClient.NETFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Add a service reference for:
            //https://www.dataaccess.com/webservicesserver/numberconversion.wso?WSDL

            var converter = new NumberConversionSoapTypeClient();
            converter.NumberToDollars(10.0m);
            
        }
    }
}
