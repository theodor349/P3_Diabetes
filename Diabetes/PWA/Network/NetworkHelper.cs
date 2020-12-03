using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PWA.Network
{
    public class NetworkHelper
    {
        private readonly HttpClient _client;

        public NetworkHelper(HttpClient client)
        {
            _client = client;
        }


    }
}
