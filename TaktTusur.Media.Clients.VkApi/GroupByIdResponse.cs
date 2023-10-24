namespace TaktTusur.Media.Clients.VkApi;

public class GroupByIdResponse
{
    public Response response { get; set; }
}

public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public string screen_name { get; set; }
        public int is_closed { get; set; }
        public string type { get; set; }
        public string photo_50 { get; set; }
        public string photo_100 { get; set; }
        public string photo_200 { get; set; }
    }

    public class Response
    {
        public List<Group> groups { get; set; }
        public List<object> profiles { get; set; }
    }

