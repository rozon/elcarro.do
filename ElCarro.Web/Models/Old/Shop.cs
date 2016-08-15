using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Shop")]
    public class Shop
    {
        public Shop()
        {
            StoreItems = new HashSet<StoreItem>();
            Reviews = new HashSet<Review>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string RNC { get; set; }
        public byte[] Images { get; set; }

        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual User User { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<StoreItem> StoreItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
