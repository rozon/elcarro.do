namespace ElCarro.Web.Models
{
    public class VehiclePart
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public virtual Company Company { get; set; }
        public virtual Model Model { get; set; }
    }
}