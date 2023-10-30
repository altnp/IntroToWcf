//Add a service reference for:
//https://www.dataaccess.com/webservicesserver/numberconversion.wso?WSDL

using NumberConversion;
using System.ServiceModel;

var client = new NumberConversionSoapTypeClient(new BasicHttpBinding(), new EndpointAddress("https://www.dataaccess.com/webservicesserver/numberconversion.wso?WSDL"));
var dollars = await client.NumberToDollarsAsync(10.0m);