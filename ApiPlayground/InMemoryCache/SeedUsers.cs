﻿using ApiPlayground.Infrastructure.Security.Hashing;
using ApiPlayground.Models;
using ApiPlayground.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.InMemoryCache
{
    public class SeedUsers
    {
        private readonly SaltGenerator _saltGenerator;
        private readonly PasswordService _passwordService;

        public SeedUsers(SaltGenerator saltGenerator, PasswordService passwordService)
        {
            _saltGenerator = saltGenerator;
            _passwordService = passwordService;
        }



        /// <summary>
        /// Seeds the allowed users for the application. This is plain string and name just to remember.
        /// The challenge will happen against the UserSecure class.
        /// </summary>
        public ICollection<User> GetAllowedUsers()
        {
            List<User> users = new List<User>
            {
                new User {Name = "Nadar", Password = "Nadar"},
                new User {Name = "Daria", Password = "Daria"},
                new User {Name = "John", Password = "John"},
                new User {Name = "Doe", Password = "Doe"}
            };

            return users;
        }


        /// <summary>
        /// Get's the user data that would be stored in a data store
        /// </summary>
        public ICollection<SecureUser> GetSecureUsers()
        {
            List<SecureUser> secureUsers = new List<SecureUser>();
            ICollection<User> users = GetAllowedUsers();

            foreach (User user in users)
            {
                string userSalt = _saltGenerator.GenerateSalt();
                SecureUser secureUser = new SecureUser
                {
                    Name = user.Name,
                    Salt = userSalt,
                    Password = _passwordService.GeneratePassword(user.Password, userSalt)
                };

                secureUsers.Add(secureUser);
            }

            return secureUsers;
        }


    }
}
