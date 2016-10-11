namespace ElCarro.Web.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Make Make { get; set; }
    }
}