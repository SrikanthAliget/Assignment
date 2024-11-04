using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp8
{
    internal class Program
    {
        static string NICjson = @"{
        'Device': 'Arista',
        'Model': 'X-Video',
        'NIC': [{
            'Description': 'Linksys ABR',
            'MAC': '14:91:82:3C:D6:7D',
            'Timestamp': '2020-03-23T18:25:43.511Z',
            'Rx': '3698574500',
            'Tx': '122558800'
            }]
        }";

        static void Main(string[] args)
        {
            JObject deviceData = JObject.Parse(NICjson);
            var nicCard = deviceData["NIC"][0];

            long rxBytes = long.Parse(nicCard["Rx"].ToString());
            long txBytes = long.Parse(nicCard["Tx"].ToString());


            Console.WriteLine("please enter the polling rate in Hz");
            double pollingRate = double.Parse(Console.ReadLine());

            double interval = CalculateInterval(pollingRate);
            double rxBitrate, txBitrate;
            if (interval > 0)
            {
                rxBitrate = (rxBytes * 8) / interval;
                txBitrate = (txBytes * 8) / interval;
                Console.WriteLine($"Rx Bitrate: {rxBitrate} bits/sec and Tx Bitrate: {txBitrate} bits/sec ");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("polling rate must be greater than 0");
                Console.ReadLine();
            }


        }

        private static double CalculateInterval(double pollingRate)
        {
            if (pollingRate <= 0)
            {
                return 0;
            }

            return 1.0 / pollingRate;
        }
    }

}
