using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IMaterialService
    {
        Task IncreaseMaterialQuantityAsync(int materialId, int amount);
        Task ReduceMaterialQuantityAsync(int materialId, int amount);
        Task DeleteMaterialAsync(int id);
        void UpdateMaterial(Material material);
        Task AddMaterialAsync(Material material);
        Task<IEnumerable<Material>> GetAllMaterialsAsync();
        Task<Material> GetMaterialByIdAsync(int id);

    }
}
