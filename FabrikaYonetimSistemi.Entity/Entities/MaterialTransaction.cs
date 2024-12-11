using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class MaterialTransaction : BaseEntity
    {
        public ActionType Action { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        public int PersonelId { get; set; }
        public Personnel Personnel { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
