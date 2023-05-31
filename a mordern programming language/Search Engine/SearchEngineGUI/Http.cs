using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchEngineGUI
{
    public class Http
    {
        /// <summary>
        /// Makes HTTP GET requests
        /// </summary>
        /// <param name="url">The endpoint to send a GET request</param>
        /// <param name="parameters"></param>
        /// <returns>The body of the HTTP response</returns>
        public static async Task<string> RequestAsync(string url, Dictionary<string, string> parameters)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(BuildEndpoint(url, parameters));
                    return response;
                }
                catch (HttpException exception) { throw exception; }
            }
        }

        /// <summary>
        /// Url encode and add GET parameters to a URL
        /// </summary>
        /// <param name="url">The url to be visited</param>
        /// <param name="parameters">The GET parameters to be added to the url</param>
        /// <returns>An encoded URL string</returns>
        private static string BuildEndpoint(string url, Dictionary<string, string> parameters)
        {
            StringBuilder endpoint = new StringBuilder();
            int numberOfParameters = parameters.Count;

            if (numberOfParameters > 0)
                endpoint.Append(url + "?");
            else
                endpoint.Append(url);


            foreach (KeyValuePair<string, string> entry in parameters)
            {
                if (numberOfParameters > 1)
                    endpoint.Append(entry.Key + "=" + HttpUtility.UrlEncode(entry.Value) + "&");
                else
                    endpoint.Append(entry.Key + "=" + HttpUtility.UrlEncode(entry.Value));

                numberOfParameters--;
            }

            return endpoint.ToString();
        }
    }
}
