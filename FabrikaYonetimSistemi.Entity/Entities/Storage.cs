using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Storage : BaseEntity
    {
        public string Name { get; set; }

        public int BuildingId { get; set; }
        public Building Building { get; set; }

        public ICollection<StorageMaterial> StorageMaterials { get; set; } = new List<StorageMaterial>();
    }
}
