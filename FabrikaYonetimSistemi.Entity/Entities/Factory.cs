using FabrikaYonetimSistemi.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FabrikaYonetimSistemi.Entity.Entities
{
    public class Factory : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Building> Buildings { get; set; } = new List<Building>();
    }
}
