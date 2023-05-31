using SearchEngine;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SearchEngineAPI.Controllers
{
    public class SearchController : ApiController
    {
        /// <summary>
        /// Endpoint for querying search engine; GET api/search?query=
        /// </summary>
        /// <param name="query">The query supplied by the user</param>
        /// <returns>Documents relevant to the users query</returns>
        public HttpResponseMessage Get([FromUri] string query)
        {
            var response = new HttpResponseMessage();
            try
            {
                var responseBody = Engine.FindMatch(new Query(HttpUtility.UrlDecode(query)));
                response.Content = new StringContent(responseBody, System.Text.Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
        }
    }
}
