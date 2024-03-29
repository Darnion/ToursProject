﻿using ToursProject.Context.Enums;
using ToursProject.Context.Models;

namespace ToursProject.UI
{
    internal static class CurrentUser
    {
        private static User user;

        public static User User
        {
            get 
            { 
                if(user == null)
                {
                    user = new User()
                    {
                        Id = -1,
                        FirstName = "Гость",
                        LastName = string.Empty,
                        Patronymic = string.Empty,
                        Role = Role.Guest
                    };
                }
                return user;
            }
            set { user = value; }
        }

        internal static bool CompareRole(Role role)
         => role == User.Role;
    }
}
