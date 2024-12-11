using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Storage : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int BuildingId { get; set; }
        public Building Building { get; set; }

        public ICollection<Material> Materials { get; set; }
    }
}
