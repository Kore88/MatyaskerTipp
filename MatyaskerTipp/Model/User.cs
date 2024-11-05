using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatyaskerTipp.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public User(int id, string userName, string password, string email, string realName, bool isActive, bool isAdmin)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Email = email;
            RealName = realName;
            IsActive = isActive;
            IsAdmin = isAdmin;
        }
    }
}
