using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToursProject.Context.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int CountOfStars { get; set; }

        [Required]
        public string CountryCode { get; set; }
        public virtual Country Country { get; set; }
        public string Description { get; set; }

        public virtual ICollection<HotelComment> HotelComments { get; set; }
        public virtual ICollection<Tour> Tours { get; set;}    

        public Hotel()
        {
            Tours = new List<Tour>();
        }
    }
}
