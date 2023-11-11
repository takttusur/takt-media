using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaktTusur.Media.Clients.VkApi
{
    public class WallByIdRequest
    {
        //TODO: сделать в camel, как я понял [JsonPropertyName("...")] работает для присылаемого JSON, а как сделать для запроса я пока не нашел
        public string domain { get; set; }
        public string access_token { get; set; }
        public string v { get; set; }
        public int count { get; set; }
        public int extended { get; set; }
    }
}
