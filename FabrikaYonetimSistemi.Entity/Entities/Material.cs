using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Material : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<StorageMaterial> StorageMaterials { get; set; } = new List<StorageMaterial>();
    }
}
