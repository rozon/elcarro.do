using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PorPartes.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        public Vehicle()
        {
            ModelVehicles = new HashSet<ModelVehicle>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Brand { get; set; }

        public virtual ICollection<ModelVehicle> ModelVehicles { get; set; }
    }
}
