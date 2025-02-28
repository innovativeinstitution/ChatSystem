using System.Text.Json.Serialization;

namespace ChatTest
{
    public class ChatResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; }
    }
}