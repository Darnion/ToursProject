using ToursProject.Context.Enums;
using ToursProject.Context.Models;

namespace ToursProject
{
    internal static class WorkToUser
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
                        FirstName = "Неавторизованный гость",
                        LastName = string.Empty,
                        Patronymic = string.Empty,
                        Role = Role.Quest
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
