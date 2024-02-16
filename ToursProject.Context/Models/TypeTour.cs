using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToursProject.Context.Models
{
    public class TypeTour
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }

        public TypeTour()
        {
            Tours = new List<Tour>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
