using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    [Table("Part_Types")]
    public class PartType
    {
        public PartType()
        {
            VehicleParts = new HashSet<VehiclePart>();
        }

        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public virtual ICollection<VehiclePart> VehicleParts { get; set; }
    }
}
