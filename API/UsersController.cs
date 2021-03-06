﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusiCoLab2.Models;
using MusiCoLab2.Services;
using MusiCoLab2.Modals;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusiCoLab2.API
{
    [Route("api/[controller]")]
    public class UserController : Controller

    {
        private UserContext context;

        public UserController(UserContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public String Get()
        {
            return "hello";
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Users/register
        [HttpPost]
        public string Post([FromBody]User user)
        {
            User foundUser = context.Users.SingleOrDefault<User>(u => u.Username == user.Username);
            if (foundUser != null)
            {
                return "Username not Availabel";
            }

            user.Salt = Auth.GenerateSalt();
            user.Password = Auth.Hash(user.Password, user.Salt);
            context.Users.Add(user);
            context.SaveChanges();
            return Auth.GenerateJWT(user);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            User foundUser = context.Users.SingleOrDefault<User>(
                u => u.Username == user.Username && u.Password == Auth.Hash(user.Password, u.Salt)
                );
            if (foundUser != null)
            {
                LoginVM vm = new LoginVM();
                vm.Id = foundUser.Id;
                vm.UserName = foundUser.Username;
               // string id = foundUser.Id.ToString();
                return Ok(vm);
                //return Auth.GenerateJWT(foundUser);
            }
            return Ok("Username and password do not match");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
