using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class MaterialRequest : BaseEntity
    {
        public int Quantity { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ApprovedTime { get; set; }
        public StorageMaterial StorageMaterial { get; set; }
        public int StorageMaterialId { get; set; }
        public Personnel Personnel { get; set; }
        public int PersonnelId { get; set; }
    }
}
