using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IStorageMaterialService
    {
        Task<StorageMaterial> GetStorageMaterialByIdAsync(int id);
        Task AddStorageMaterialAsync(StorageMaterial storageMaterial);
        Task UpdateStorageMaterialAsync(StorageMaterial storageMaterial);
        Task<IEnumerable<StorageMaterial>> GetAllStorageMaterialAsync();
        Task<StorageMaterial> GetByMaterialAndStorageAsync(int materialId, int storageId);
        Task<IEnumerable<Storage>> GetAllStoragesWithMaterialAsync(int materialId);
        Task<IEnumerable<Material>> GetMaterialsByStorageIdAsync(int storageId);
        Task<IEnumerable<StorageMaterial>> GetStorageMaterialsByStorageIdAsync(int storageId);
        
        
    }
}
