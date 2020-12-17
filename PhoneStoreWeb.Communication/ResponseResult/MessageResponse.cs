namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class MessageResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public MessageResponse(string type, string title, string content = "")
        {
            Type = type;
            Title = title;
            Content = content;
        }
    }
}
