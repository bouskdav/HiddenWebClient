using System;
using System.IO;
using System.Linq;
using System.Net;

namespace HiddenWebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("Spuštěno " + DateTime.Now);
            Console.WriteLine("----------------------");

            try
            {
                if (args.Count() == 0)
                {
                    throw new Exception("Není zadána URL.");
                }

                string url = args[0];

                Console.WriteLine("URL: " + url);
                Console.WriteLine("Odpověď:");

                string response = Get(url);

                Console.WriteLine(response);
            }
            catch(Exception ex)
            {
                Console.WriteLine(String.Format("Chyba: {0}", ex.Message));
            }
            finally
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Konec " + DateTime.Now);
                Console.WriteLine("----------------------");
            }
        }

        private static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
