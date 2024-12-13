using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IStorageService
    {
        Task<Storage> GetStorageByIdAsync(int id);
        Task<IEnumerable<Storage>> GetAllStoragesAsync();
        Task AddStorageAsync(Storage storage);
        void UpdateStorage(Storage storage);
        Task DeleteStorageAsync(int id);
        Task<ICollection<Material>> GetMaterialsInStorageAsync(int storageId);
    }
}
