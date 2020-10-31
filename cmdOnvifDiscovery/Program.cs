

using System;
using System.ServiceModel.Discovery;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

namespace cmdOnvifDiscovery
{    class Program
    {
        public static void Main(string[] args)
        {
            var endPoint = new UdpDiscoveryEndpoint(DiscoveryVersion.WSDiscoveryApril2005);
            var discoveryClient = new System.ServiceModel.Discovery.DiscoveryClient(endPoint);
            
            var findCriteria = new FindCriteria
            {
                Duration = TimeSpan.FromSeconds(3),
                MaxResults = int.MaxValue
            };

            findCriteria.ContractTypeNames.Add(new XmlQualifiedName("NetworkVideoTransmitter", @"http://www.onvif.org/ver10/network/wsdl"));

            Console.WriteLine("Initiating find operation.");
            
            var response = discoveryClient.Find(findCriteria);
            List<string> lst = new List<string>();

            foreach (var e in response.Endpoints)
            {
                
                foreach (var item in e.ListenUris)
                {
                    string uri = item.OriginalString;
                    string host = item.Host;
                    lst.Add(host + ": " + uri);
                    
                }
            }
            lst = lst.Distinct().ToList();
            lst.ForEach(x => Console.WriteLine(x));

            Console.WriteLine($"Operation returned - Found {lst.Count} endpoints.");
            Console.ReadKey();
        }

        
        
    }

}