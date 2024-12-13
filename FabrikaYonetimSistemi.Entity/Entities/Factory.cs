using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Factory : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Building> Buildings { get; set; } = new List<Building>();
    }
}
