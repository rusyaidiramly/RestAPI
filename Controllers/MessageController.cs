using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RestAPI.Models;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [Route("api/User/{userid}/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public static readonly string fileName = "MessageList.json";
        private static List<Message> Messages = JsonFileService.LoadJsonFile<Message>(fileName);

        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return Messages.FindAll(
                    message => message.AuthorID == int.Parse(RouteData.Values["userid"].ToString())
            );
        }

        [HttpGet("{id}")]
        public Message Get(int id)
        {
            return Messages.Find(message
                    => message.MessageID == id
                    && message.AuthorID == int.Parse(RouteData.Values["userid"].ToString())
            );
        }

        [HttpPost]
        public void Post([FromBody] Message value)
        {
            int currentID = (Messages?.Any() == true) ? Messages.Last().MessageID : 0;
            Messages.Add(new Message
            {
                MessageID = currentID + 1,
                AuthorID = int.Parse(RouteData.Values["userid"].ToString()),
                PlainMessage = value.PlainMessage
            });

            JsonFileService.SaveJsonFile(Messages, fileName);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Message value)
        {
            Message selectedMessage = Messages.Find(message
                                        => message.MessageID == id
                                        && message.AuthorID == int.Parse(RouteData.Values["userid"].ToString())
                                    );

            if (selectedMessage == null) return;
            if (value.PlainMessage != null) selectedMessage.PlainMessage = value.PlainMessage;

            JsonFileService.SaveJsonFile(Messages, fileName);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Message selectedMessage = Messages.Find(message
                                        => message.MessageID == id
                                        && message.AuthorID == int.Parse(RouteData.Values["userid"].ToString())
                                    );

            if (selectedMessage == null) return;
            Messages.Remove(selectedMessage);

            JsonFileService.SaveJsonFile(Messages, fileName);
        }
    }
}
