using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToursProject.Context.Models
{
    public class ReceivingPoint
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
         => $"{Title}  {Address}";
    }
}
