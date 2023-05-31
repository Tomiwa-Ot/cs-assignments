using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineGUI
{
    public class Endpoint
    {
        public const string DOMAIN = "localhost";
        public const string PORT = "44303";
        public const string URL = "https://" + DOMAIN + ":" + PORT + "/api";
        public const string SEARCH = URL + "/search";
    }
}
