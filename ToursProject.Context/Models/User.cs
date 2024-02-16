using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToursProject.Context.Enums;

namespace ToursProject.Context.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        public virtual ICollection<HotelComment> HotelComments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {Patronymic}";
        }
    }
}
