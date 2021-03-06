using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sa_webapp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text;


namespace sa_webapp.Services
{
    public class MessageService
    {
        private String flaskUrl = "http://localhost:8080/chatbot";
        private HttpClient client = new HttpClient();
        private HttpRequestMessage requestMessage;

        public Message ask(Message msg)
        {
            requestMessage = new HttpRequestMessage(HttpMethod.Post, flaskUrl);
            // Should get the answer from Flask
            String msgJson = JsonSerializer.Serialize(msg);

            requestMessage.Content = new StringContent(msgJson, Encoding.Unicode, "application/json");
            HttpResponseMessage response = client.SendAsync(requestMessage).GetAwaiter().GetResult();

            if (response.StatusCode.ToString() == "OK")
            {
                string r = response.Content.ReadAsStringAsync().Result.ToString();
                Message ms = JsonSerializer.Deserialize<Message>(r);
                return ms;
            }
            else {
                return new Message();
            }
            // Console.WriteLine("In ask Method");
            // return new Message();
        }
    }
}