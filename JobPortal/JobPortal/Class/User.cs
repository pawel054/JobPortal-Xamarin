﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Class
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public User(int UserID, string UserEmail, string UserPassword, bool IsAdmin)
        {
            this.UserID = UserID;
            this.Email = UserEmail;
            this.Password = UserPassword;
            this.IsAdmin = IsAdmin;
        }

        public User(string UserEmail, string UserPassword)
        {
            this.Email = UserEmail;
            this.Password = UserPassword;
        }

        public User(string UserEmail, string UserPassword, bool IsAdmin)
        {
            this.Email = UserEmail;
            this.Password = UserPassword;
            this.IsAdmin = IsAdmin;
        }
    }
}
