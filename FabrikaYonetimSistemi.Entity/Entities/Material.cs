using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Material : BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public int StorageId { get; set; }
        public Storage Storage { get; set; }

        public ICollection<MaterialTransaction> MaterialTransactions { get; set; }
    }
}
