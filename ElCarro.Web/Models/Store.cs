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

        public Store(StoreView store)
        {
            Name = store.Name;
            PhoneNumber = store.PhoneNumber;
            Email = store.Email;
            CompanyId = store.CompanyId;
            latitude = store.lat_long.lat;
            longitude = store.lat_long.lng;
        }

        public int StoreID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(75)]
        public string Name { get; set; }
        public string Logo { get; set; }
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

    [NotMapped]
    public class StoreView
    {
        public StoreView() { }

        public StoreView(Store store)
        {
            StoreID = store.StoreID;
            Name = store.Name;
            PhoneNumber = store.PhoneNumber;
            Email = store.Email;
            CompanyId = store.CompanyId;
            lat_long = new pos(store.latitude, store.longitude);
        }

        public int StoreID { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        public HttpPostedFileBase Logo { get; set; }
        [Required]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La ubicación es requerida.")]
        public pos lat_long { get; set; }
        public int CompanyId { get; set; }
    }

    [NotMapped]
    public class pos
    {
        public pos() { }

        public pos(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }

        public double lat { get; set; }
        public double lng { get; set; }
    }
}
