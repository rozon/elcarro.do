using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ElCarro.Web.Models
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            ActualPage = ActualPage <= 0 ? 1 : ActualPage;
        }

        public string NameOrDescription { get; set; }

        public int Model { get; set; }

        public int Make { get; set; }

        public int Year { get; set; }

        public int FromPage { get; set; }

        public int ToPage { get; set; }

        public bool NoResult => Total == 0;

        public IEnumerable<SelectListItem> Makes { get; set; }

        public IEnumerable<SelectListItem> Models { get; set; }

        public int Total { get; set; }

        public int ActualPage { get; set; }

        public const int ResultsPerPages = 5;

        public void ConfigPaginator()
        {
            FromPage = ActualPage - 2 <= 1 ? 1 : ActualPage - 2;
            ToPage =
                (ActualPage + 2) > (Total / ResultsPerPages) ? (Total / ResultsPerPages) + 1 :
                ActualPage + 2 <= ResultsPerPages ? ResultsPerPages : ResultsPerPages + 2;
        }

        public IEnumerable<SelectListItem> Years =>
            new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text="Select a Year",
                    Value = 0.ToString(),
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

        public IEnumerable<VehiclePart> Results { get; set; }

        public void FillDropDowns(IEnumerable<Make> makes,
           IQueryable<Model> models)
        {
            FillMakes(makes);
            FillModels(models);
        }

        private void FillMakes(IEnumerable<Make> makes)
        {
            var data = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text="Select a Make",
                    Value = 0.ToString(),
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
                    Text = "Select a Model",
                    Value = 0.ToString(),
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
    }
}