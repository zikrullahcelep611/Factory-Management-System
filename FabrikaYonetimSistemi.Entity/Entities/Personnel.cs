using FabrikaYonetimSistemi.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Personnel : IdentityUser<int>, IBaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public ICollection<MaterialTransaction> MaterialTransactions { get; set; } = new List<MaterialTransaction>();
        public ICollection<MaterialRequest> MaterialRequests { get; set; } = new List<MaterialRequest>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
