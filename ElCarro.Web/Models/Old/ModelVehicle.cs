using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Model_Vehicle")]
    public class ModelVehicle
    {
        public ModelVehicle()
        {
            StoreItems = new HashSet<StoreItem>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Model { get; set; }
        [Required]
        public DateTime Year { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Vehicle Vehicule { get; set; }

        public virtual ICollection<StoreItem> StoreItems { get; set; }
    }
}
