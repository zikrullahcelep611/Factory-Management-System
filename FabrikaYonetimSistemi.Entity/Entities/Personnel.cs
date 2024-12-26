using FabrikaYonetimSistemi.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Personnel : IdentityUser<int>, IBaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<MaterialTransaction> MaterialTransactions { get; set; } = new List<MaterialTransaction>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
