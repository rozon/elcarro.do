using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCarro.Web.Models
{
    public class CreateVehiclePart
    {
        public CreateVehiclePart()
        {
            Models = new List<SelectListItem>() {  new SelectListItem
                {
                    Text="Select a Model",
                    Value=null,
                    Selected = true
                }
            };
            Makes = new List<SelectListItem>();
            Stores = new List<SelectListItem>();
        }

        public CreateVehiclePart(IEnumerable<Make> makes, IEnumerable<Store> stores)
                : this()
        {
            Makes = MakesSelect(makes);
            Stores = StoreSelect(stores);
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public int Model { get; set; }

        public int Make { get; set; }

        public int Store { get; set; }

        public IEnumerable<SelectListItem> Makes { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public IEnumerable<SelectListItem> Stores { get; set; }


        public HttpPostedFileBase Photo { get; set; }

        public static IEnumerable<SelectListItem> MakesSelect(IEnumerable<Make> makes, int? selectedValue = null)
        {
            var data = new List<SelectListItem>()
            {
                //The default option, no selectable.
                new SelectListItem
                {
                    Text="Select a Make",
                    Value=null,
                    Selected = selectedValue == null,
                }
            };
            if (makes != null)
                data.AddRange(makes.Select(m => new SelectListItem()
                {
                    Text = m.Name,
                    Value = m.Id.ToString(),
                    Selected = selectedValue != null ? m.Id == selectedValue : false,
                }));

            return data;
        }

        public static IEnumerable<SelectListItem> StoreSelect(IEnumerable<Store> stores, int? selectedValue = null)
        {
            var data = new List<SelectListItem>()
            {
                //The default option, no selectable.
                new SelectListItem
                {
                    Text="Select a Store",
                    Value = null,
                    Selected = selectedValue == null,
                }
            };
            if (stores != null)
                data.AddRange(stores.Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.StoreID.ToString(),
                    Selected = selectedValue != null ? s.StoreID == selectedValue : false,
                }));

            return data;
        }

    }
}