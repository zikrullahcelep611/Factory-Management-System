using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IMaterialService
    {
        Task DeleteMaterialAsync(Material material);
        void UpdateMaterial(Material material);
        Task AddMaterialAsync(Material material);
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task<Material> GetMaterialByIdAsync(int id);
    }
}
