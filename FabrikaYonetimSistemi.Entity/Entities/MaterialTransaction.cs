using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class MaterialTransaction : BaseEntity
    {
        public int StorageMaterialId { get; set; }
        public StorageMaterial StorageMaterial {get;set;}
        public int PersonnelId { get; set; }
        public Personnel Personnel { get; set; }

        public ActionType TransactionType { get; set; }
        public int QuantityChange { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}
