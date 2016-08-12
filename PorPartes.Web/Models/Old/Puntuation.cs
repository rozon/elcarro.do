using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Puntuation")]
    public class Puntuation
    {
        public Puntuation()
        {
            Reviews = new HashSet<Review>();
        }

        public int ID { get; set; }
        [Required]
        public decimal Level { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
