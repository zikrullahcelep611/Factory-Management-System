using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class StorageService
    {
        private readonly IRepository<Storage> _storageRepository;

        public StorageService(IRepository<Storage> storageRepository)
        {
            _storageRepository = storageRepository;
        }

        // Get Storage by ID
        public async Task<Storage> GetStorageByIdAsync(int id)
        {
            var storage = await _storageRepository.GetByIdAsync(id);
            if (storage == null)
            {
                throw new Exception($"Storage with ID {id} not found.");
            }
            return storage;
        }

        // Get all Storages
        public async Task<IEnumerable<Storage>> GetAllStoragesAsync()
        {
            return await _storageRepository.GetAllAsync();
        }

        // Add new Storage
        public async Task AddStorageAsync(Storage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            storage.CreatedAt = DateTime.UtcNow;
            await _storageRepository.AddAsync(storage);
        }

        // Update existing Storage
        public void UpdateStorage(Storage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            _storageRepository.Update(storage);
        }

        // Delete Storage by ID
        public async Task DeleteStorageAsync(int id)
        {
            var storage = await _storageRepository.GetByIdAsync(id);
            if (storage == null)
            {
                throw new Exception($"Storage with ID {id} not found.");
            }

            _storageRepository.Delete(storage);
        }

        // Get Materials in a Storage
        public async Task<ICollection<Material>> GetMaterialsInStorageAsync(int storageId)
        {
            var storage = await GetStorageByIdAsync(storageId);
            return storage.Materials;
        }

        // Check if a Storage Exists by ID
        public async Task<bool> StorageExistsAsync(int id)
        {
            var storage = await _storageRepository.GetByIdAsync(id);
            return storage != null;
        }
    }
}
