using System.Collections.Generic;

namespace ElCarro.Web.Models
{
    public class PartView
    {
        public PartView() { }

        public string NameOrDescription { get; set; }

        public int Model { get; set; }

        public int Make { get; set; }

        public int Year { get; set; }
    }

    public class ResultSingleSearch
    {
        public ResultSingleSearch() { }

        public IEnumerable<VehiclePartView> Parts { get; set; }
        public int TotalParts { get; set; }
    }
}
