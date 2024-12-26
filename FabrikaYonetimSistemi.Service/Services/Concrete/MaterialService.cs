using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> _materialRepository;

        public MaterialService(IRepository<Material> materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task AddMaterialAsync(Material material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));

            await _materialRepository.AddAsync(material);
        }

        public async Task DeleteMaterialAsync(Material material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));

            _materialRepository.Delete(material);
        }

        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            //Burda ilgili entity'leride getirmek icin kontrol etmek gerekiyor.
            return await _materialRepository.GetAllAsync();
        }

        public async Task<Material> GetMaterialByIdAsync(int id)
        {
            return await _materialRepository.GetByIdAsync(id);
        }

        public void UpdateMaterial(Material material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));

            _materialRepository.Update(material);
        }
    }
}
