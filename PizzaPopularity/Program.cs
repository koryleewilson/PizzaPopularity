using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System;
using PizzaPopularity.Models;

namespace PizzaPopularity
{
    /// <summary>
    /// Test class for getting top 20 most popular bikes
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Our input Json is here
        /// </summary>
        private const string POPULARITYRESPONSES = @"http://files.olo.com/pizzas.json";

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">input is ignored</param>
        static void Main(string[] args)
        {
            // read in the data - it is json
            string popularityData = ReadInPopularityData();

            //deserialize the Json
            PizzaChoice[] toppingsList = JsonConvert.DeserializeObject<PizzaChoice[]>(popularityData);

            //Get the 20 most popular combinations
            List<string> popularPizzas = GetMostPopular(toppingsList, 20);

            int rank = 1;
            List<string> output = new List<string>();

            popularPizzas.ToList().ForEach(p => output.Add(p += $"\r\n rank {rank++}\r\n"));

            // output the results
            FileStream fs = File.Create("TopTwenty.txt");
            StreamWriter sw = new StreamWriter(fs);
            output.ForEach(pb => sw.WriteLine(pb));

            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Inspect the array and return the most repeated
        /// </summary>
        /// <param name="pizzaChoice">The array to inspect</param>
        /// <param name="count">count to return</param>
        /// <returns>The most repeated Purchased objects</returns>
        public static List<string> GetMostPopular(PizzaChoice[] pizzaChoice, int count)
        {
            //creat a dictionary container
            Dictionary<PizzaChoice, int> purchCount = new Dictionary<PizzaChoice, int>();
            foreach (PizzaChoice purch in pizzaChoice)
            {
                // add the count of each topping combination
                if (!purchCount.ContainsKey(purch))
                {
                    purchCount.Add(purch, 1);
                }
                else
                {
                    purchCount[purch]++;
                }
            }

            // compile the results
            return purchCount?.OrderByDescending(pc => pc.Value).Select(v => $"Toppings:{v.Key} ordered {v.Value} number of times").Take(count).ToList();
        }

        /// <summary>
        /// Read in the popularity data
        /// </summary>
        /// <returns>Json data</returns>
        private static string ReadInPopularityData()
        {
            HttpWebRequest request = (HttpWebRequest)

            // get the data url
            WebRequest.Create(POPULARITYRESPONSES);

            // execute the request
            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            // create a buffer
            int size = 1000;
            byte[] buf = new byte[size];

            // create our string builder
            StringBuilder sb = new StringBuilder();

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            // return the data
            return sb.ToString();
        }
    }
}
