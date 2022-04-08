using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using RestAPI.Models;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public static readonly string fileName = "MessageList.json";
        private static List<Message> Messages = JsonFileService.LoadJsonFile<Message>(fileName);


        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return Messages;
        }

        [HttpGet("{id}")]
        public Message Get(int id)
        {
            return Messages.Find(Message => Message.MessageID == id);
        }

        [HttpPost]
        public void Post([FromBody] Message value)
        {
            int currentID = (Messages?.Any() == true) ? Messages.Last().MessageID : 0;
            Messages.Add(new Message
            {
                MessageID = currentID + 1,
                AuthorID = value.AuthorID,
                PlainMessage = value.PlainMessage
            });

            JsonFileService.SaveJsonFile(Messages, fileName);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Message value)
        {
            Message selectedMessage = Messages.Find(Message => Message.MessageID == id);
            if (selectedMessage == null) return;
            if (value.PlainMessage != null) selectedMessage.PlainMessage = value.PlainMessage;

            JsonFileService.SaveJsonFile(Messages, fileName);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Message selectedMessage = Messages.Find(Message => Message.MessageID == id);
            if (selectedMessage == null) return;
            Messages.Remove(selectedMessage);

            JsonFileService.SaveJsonFile(Messages, fileName);
        }
    }
}
