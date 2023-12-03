using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaktTusur.Media.Clients.VkApi
{
    public class WallByIdRequest
    {
        
        [RequestProperty(Name = "domain")]
        public string Domain { get; set; }

        [RequestProperty(Name = "access_token")]
        public string AccessToken { get; set; }

        [RequestProperty(Name = "v")]
        public string Version { get; set; }

        [RequestProperty(Name = "count")]
        public int Count { get; set; }

        [RequestProperty(Name = "extended")]
        public int Extended { get; set; }
    }
}
