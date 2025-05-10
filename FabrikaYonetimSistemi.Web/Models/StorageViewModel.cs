using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Web.Models
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Building Building { get; set; }
    }
}
