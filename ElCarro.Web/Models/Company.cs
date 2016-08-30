using Microsoft.AspNet.Identity;
using System.Data.Entity.Spatial;

namespace ElCarro.Web.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; internal set; }
        public string Address { get; set; }
        public DbGeography Location { get; set; }
        public ApplicationUser Admin { get; set; }
    }
}