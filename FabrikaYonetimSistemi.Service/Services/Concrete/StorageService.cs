using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class StorageService : IStorageService
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
            return await _storageRepository.GetAllAsync(s => s.Building);
        }

        // Add new Storage
        public async Task AddStorageAsync(Storage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

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
        public async Task DeleteStorageAsync(Storage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            _storageRepository.Delete(storage);
        }

        public Task<ICollection<Material>> GetMaterialsInStorageAsync(int storageId)
        {
            throw new NotImplementedException();
        }
    }
}
