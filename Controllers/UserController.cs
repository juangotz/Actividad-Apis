﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Model;
using Api.Controllers.DTO;
using Api.Repository;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("UserList")]
        public static List<User> GetUsers()
        {
            return UserHandler.GetUsers();
        }
        [HttpDelete]
        public bool DeleteUser([FromBody] int id)
        {
            try
            {
                return UserHandler.DeleteUser(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPut]
        public bool UpdateUser([FromBody] PutUser user)
        {
            try
            {
                return UserHandler.UpdateNameUser(new User
                {
                    id = user.id,
                    name = user.name
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpPost]
        public bool CreateUser([FromBody] PostUser user)
        {
            try
            {
                return UserHandler.CreateUser(new User
                {
                    name = user.name,
                    surname = user.surname,
                    userName = user.userName,
                    password = user.password,
                    email = user.email
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        [HttpGet]
        public bool LoginMethod(string userName, string password)
        {
            try
            {
                User user = UserHandler.LoginMethod(userName, password);
                if (user.name == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}