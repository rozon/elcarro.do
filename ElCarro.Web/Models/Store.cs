using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ElCarro.Web.Models
{
    [Table("Stores")]
    public class Store
    {
        public Store()
        {
            VehicleParts = new HashSet<VehiclePart>();
            Reviews = new HashSet<Review>();
        }

        public int StoreID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(75)]
        public string Name { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        [Display(Name = "Logo")]
        public HttpPostedFileBase LogoFile { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La ubicación es requerida.")]
        public double latitude { get; set; }
        [Required(ErrorMessage = "La ubicación es requerida.")]
        public double longitude { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        public virtual ICollection<VehiclePart> VehicleParts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
