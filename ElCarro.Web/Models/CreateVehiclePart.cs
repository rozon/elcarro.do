using System.Web;

namespace ElCarro.Web.Models
{
    public class CreateVehiclePart
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Photo { get; set; }
    }
}