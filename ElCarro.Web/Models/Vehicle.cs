using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        public Vehicle()
        {
            StoreItems = new HashSet<StoreItem>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Brand { get; set; }
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        [Required]
        public DateTime Year { get; set; }

        public virtual ICollection<StoreItem> StoreItems { get; set; }
    }
}
