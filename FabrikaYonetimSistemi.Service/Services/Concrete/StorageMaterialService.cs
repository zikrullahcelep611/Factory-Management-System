using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class StorageMaterialService : IStorageMaterialService
    {
        private readonly IRepository<StorageMaterial> _repository;

        public StorageMaterialService(IRepository<StorageMaterial> repository)
        {
            _repository = repository;
        }

        public async Task AddStorageMaterialAsync(StorageMaterial storageMaterial)
        {
            await _repository.AddAsync(storageMaterial);
        }

        public async Task UpdateStorageMaterialAsync(StorageMaterial storageMaterial)
        {
            _repository.Update(storageMaterial);
        }

        public async Task<StorageMaterial> GetStorageMaterialByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<StorageMaterial>> GetAllStorageMaterialAsync()
        {
            return await _repository.GetAllAsync(sm => sm.Material, sm => sm.Storage);
        }

        public async Task<StorageMaterial> GetByMaterialAndStorageAsync(int materialId, int storageId)
        {
            return await _repository.GetFirstOrDefaultAsync(sm => sm.MaterialId == materialId && sm.StorageId == storageId);
        }

        public async Task<IEnumerable<Storage>> GetAllStoragesWithMaterialAsync(int materialId)
        {
            return await _repository.GetAll()
                .Where(sm => sm.MaterialId == materialId)
                .Select(sm => sm.Storage)
                .ToListAsync();
        }

        public async Task<IEnumerable<Material>> GetMaterialsByStorageIdAsync(int storageId)
        {
            return await _repository.GetAll()
                .Where(sm => sm.StorageId == storageId)
                .Select(sm => sm.Material)
                .ToListAsync();
        }

        public async Task<IEnumerable<StorageMaterial>> GetStorageMaterialsByStorageIdAsync(int storageId)
        {
            return await _repository.GetAll()
                .Where(sm => sm.StorageId == storageId) // Depoya göre filtreleme
                .Include(sm => sm.Material) // Material ilişkisini yükle
                .ToListAsync();
        }
    }
}
