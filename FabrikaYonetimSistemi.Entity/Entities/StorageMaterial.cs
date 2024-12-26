using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class StorageMaterial : BaseEntity
    {
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public ICollection<MaterialTransaction> MaterialTransactions { get; set; } = new List<MaterialTransaction>();

        public int Quantity { get; set; }
    }
}
