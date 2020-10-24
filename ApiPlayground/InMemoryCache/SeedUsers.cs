using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.InMemoryCache
{
    public class SeedUsers
    {
        public SeedUsers()
        {

        }

        /// <summary>
        /// Seeds the allowed users for the application. This is plain string and name just to remember.
        /// The challenge will happen against the UserSecure class.
        /// </summary>
        public User[] AllowedUsers()
        {
            List<User> users = new List<User>
            {
                new User {Name = "Nadar", Password = "Nadar"},
                new User {Name = "Daria", Password = "Daria"},
                new User {Name = "John", Password = "John"},
                new User {Name = "Doe", Password = "Doe"}
            };

            return users.ToArray();
        }


    }
}
