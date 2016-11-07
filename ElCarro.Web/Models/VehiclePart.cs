using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    [Table("VehicleParts")]
    public class VehiclePart
    {
        public VehiclePart()
        {
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo es requerido")]
        [MaxLength(75)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [StringLength(1000)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Foto")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Repuesto")]
        public virtual Store Store { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Año")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Modelo")]
        public virtual Model Model { get; set; }

        [Column("Last_View")]
        public DateTime? LastView { get; set; }

        [Required]
        public int Popularity { get; set; }
    }
}
