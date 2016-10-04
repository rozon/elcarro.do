using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ElCarro.Web.Models
{
    [Table("VehicleParts")]
    public class VehiclePart
    {
        public VehiclePart()
        {
            StoreItems = new HashSet<StoreItem>();
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public string Photo { get; set; }
        public virtual Company Company { get; set; }
        public virtual Model Model { get; set; }
        [Required]
        [Column("Last_View")]
        public DateTime LastView { get; set; }
        [Required]
        public int Popularity { get; set; }

        public virtual ICollection<StoreItem> StoreItems { get; set; }
    }

    [NotMapped]
    public class CreateVehiclePart
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Photo { get; set; }
    }
}