using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Web.Models
{
    public class BuildingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Factory Factory { get; set; }
    }
}
