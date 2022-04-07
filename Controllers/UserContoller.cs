using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// using System;
using System.Security.Cryptography;
using RestAPI.Models;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> Users = JsonFileService.LoadJsonFile();

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Users;
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return Users.Find(user => user.UserID == id);
        }

        [HttpPost]
        public void Post([FromBody] User value)
        {
            int currentID = (Users.Any()) ? Users.Last().UserID : 0;
            Users.Add(new User
            {
                UserID = currentID + 1,
                Name = value.Name,
                Email = value.Email,
                Password = value.Password,
                NRIC = value.NRIC
            });

            JsonFileService.SaveJsonFile(Users);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User value)
        {
            User selectedUser = Users.Find(user => user.UserID == id);
            if (selectedUser == null) return;
            if (value.Name != null) selectedUser.Name = value.Name;
            if (value.Email != null) selectedUser.Email = value.Email;
            if (value.Password != null) selectedUser.Password = value.Password;
            if (value.NRIC != null) selectedUser.NRIC = value.NRIC;
            JsonFileService.SaveJsonFile(Users);

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User selectedUser = Users.Find(user => user.UserID == id);
            if (selectedUser == null) return;
            Users.Remove(selectedUser);
            JsonFileService.SaveJsonFile(Users);
        }
    }
}
