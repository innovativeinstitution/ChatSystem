using ChatTestMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace ChatTestMvc.Controllers
{
    public class ChatController : Controller
    {
        private readonly ILogger<ChatController> _logger;
        private readonly HttpClient _httpClient;

        public ChatController(ILogger<ChatController> logger, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskLLM(string question)
        {
            var chatRequest = new ChatRequest { Question = question };
            var jsonContent = new StringContent(JsonSerializer.Serialize(chatRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7255/api/chat/ask", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "Error communicating with API" });
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var chatResponse = JsonSerializer.Deserialize<ChatResponse>(responseData);

            return Json(new { success = true, answer = chatResponse?.Response });
        }
    }
}
