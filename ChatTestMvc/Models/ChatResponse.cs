using System.Text.Json.Serialization;

namespace ChatTestMvc.Models
{
    public class ChatResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; }
    }
}