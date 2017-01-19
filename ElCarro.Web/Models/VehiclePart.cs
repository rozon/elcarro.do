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

        [StringLength(1000)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Por favor entre un numero valido.")]
        [Required(ErrorMessage = "El precio es requerido.")]
        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Sucursal")]
        public virtual Store Store { get; set; }

        [Range(1000, 9999, ErrorMessage = "Por favor entre un numero valido.")]
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
