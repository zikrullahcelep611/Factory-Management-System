using FabrikaYonetimSistemi.Core.Entities;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Personnel : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<MaterialTransaction> MaterialTransactions { get; set; } = new List<MaterialTransaction>();
    }
}
