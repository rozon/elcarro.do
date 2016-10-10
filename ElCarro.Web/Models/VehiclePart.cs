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
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public virtual Store Store { get; set; }

        [Required]
        public virtual Model Model { get; set; }

        [Column("Last_View")]
        public DateTime? LastView { get; set; }

        [Required]
        public int Popularity { get; set; }
    }
}