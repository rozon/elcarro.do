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
        }

        public int StoreID { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        public string Logo { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [NotMapped]
        [Display(Name = "Dirección")]
        public string FullAdress => StoreAddress.StreetName + " #"
            + StoreAddress.StreetNumber + ", "
            + StoreAddress.Zone + ", "
            + StoreAddress.Province + " "
            + StoreAddress.City;

        public virtual StoreAddress StoreAddress { get; set; }
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
        public int CompanyId { get; set; }

        public StoreAddressView address { get; set; }
    }
}
