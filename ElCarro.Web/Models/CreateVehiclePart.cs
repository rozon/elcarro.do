using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElCarro.Web.Models
{
    public class CreateVehiclePart
    {
        public CreateVehiclePart()
        {
            Models = new List<SelectListItem>();
            Makes = new List<SelectListItem>();
            Stores = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public int Model { get; set; }

        [Required]
        [Display(Name = "Marca")]
        public int Make { get; set; }

        [Required]
        [Display(Name = "Repuesto")]
        public int Store { get; set; }

        [Required]
        [Display(Name = "Año")]
        public int Year { get; set; }

        public IEnumerable<SelectListItem> Years =>
            new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text="Seleccionar año",
                    Value = null,
                    Selected = Year == 0,
                }
            }
            .Concat(Enumerable.Range(1986, DateTime.Now.Year - 1986 + 1)
                .Select(y =>
                    new SelectListItem
                    {
                        Text = y.ToString(),
                        Value = y.ToString(),
                        Selected = y == Year,
                    }));

        public IEnumerable<SelectListItem> Makes { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public IEnumerable<SelectListItem> Stores { get; set; }

        [Display(Name = "Foto")]
        public HttpPostedFileBase Photo { get; set; }

        private void FillMakes(IEnumerable<Make> makes)
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "Seleccionar marca",
                    Value = null,
                    Selected = Make == 0,
                }
            };
            if (makes != null)
                data.AddRange(makes.Select(m => new SelectListItem()
                {
                    Text = m.Name,
                    Value = m.Id.ToString(),
                    Selected = Make != 0 ? m.Id == Make : false,
                }));
            Makes = data;
        }

        private void FillModels(IQueryable<Model> models)
        {
            var data = new List<SelectListItem>()
            {
                //The default option, no selectable.
                new SelectListItem
                {
                    Text = "Seleccionar modelo",
                    Value = null,
                    Selected = Model == 0,
                }
            };
            if (Make != 0)
                data.AddRange(models.Where(m => m.Make.Id == Make).Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = Model != 0 ? s.Id == Model : false,
                }).ToList());

            Models = data;
        }

        private void FillStores(IEnumerable<Store> stores)
        {
            var data = new List<SelectListItem>()
            {
                //The default option, no selectable.
                new SelectListItem
                {
                    Text="Seleccionar repuesto",
                    Value = null,
                    Selected = Store == 0,
                }
            };
            if (stores != null)
                data.AddRange(stores.Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.StoreID.ToString(),
                    Selected = Store != 0 ? s.StoreID == Store : false,
                }));

            Stores = data;
        }

        public void FillDropDowns(IEnumerable<Make> makes,
            IEnumerable<Store> stores,
            IQueryable<Model> models)
        {
            FillMakes(makes);
            FillStores(stores);
            FillModels(models);
        }

        public static CreateVehiclePart Factory(IEnumerable<Make> makes,
            IEnumerable<Store> stores,
            IQueryable<Model> models,
            VehiclePart vehiclePart)
        {
            var model = new CreateVehiclePart();
            if (vehiclePart != null)
            {
                model.Name = vehiclePart.Name;
                model.Description = vehiclePart.Description;
                model.Make = vehiclePart.Model.Make.Id;
                model.Model = vehiclePart.Model.Id;
                model.Year = vehiclePart.Year;
                model.Store = vehiclePart.Store.StoreID;
            }

            model.FillMakes(makes);
            model.FillStores(stores);
            model.FillModels(models);

            return model;
        }
    }
}