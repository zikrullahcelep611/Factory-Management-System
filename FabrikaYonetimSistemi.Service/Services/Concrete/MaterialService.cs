using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    /*public class MaterialService : IMaterialService
    {
        private readonly IRepository<Material> _materialRepository;

        public MaterialService(IRepository<Material> materialRepository)
        {
            _materialRepository = materialRepository;
        }

        // Get Material by ID
        public async Task<Material> GetMaterialByIdAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);
            if (material == null)
            {
                throw new Exception($"Material with ID {id} not found.");
            }
            return material;
        }

        // Get all Materials
        public async Task<IEnumerable<Material>> GetAllMaterialsAsync()
        {
            return await _materialRepository.GetAllAsync();
        }

        // Add new Material
        public async Task AddMaterialAsync(Material material)
        {
            if (material == null)
            {
                throw new ArgumentNullException(nameof(material));
            }

            if (material.Quantity < 0)
            {
                throw new ArgumentException("Material quantity cannot be negative.");
            }

            await _materialRepository.AddAsync(material);
        }

        // Update existing Material
        public void UpdateMaterial(Material material)
        {
            if (material == null)
            {
                throw new ArgumentNullException(nameof(material));
            }

            if (material.Quantity < 0)
            {
                throw new ArgumentException("Material quantity cannot be negative.");
            }

            _materialRepository.Update(material);
        }

        // Delete Material by ID
        public async Task DeleteMaterialAsync(int id)
        {
            var material = await _materialRepository.GetByIdAsync(id);
            if (material == null)
            {
                throw new Exception($"Material with ID {id} not found.");
            }

            _materialRepository.Delete(material);
        }

        // Reduce Material Quantity (Business Logic Example)
        public async Task ReduceMaterialQuantityAsync(int materialId, int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Reduction amount must be greater than zero.");
            }

            var material = await GetMaterialByIdAsync(materialId);
            if (material.Quantity < amount)
            {
                throw new InvalidOperationException($"Insufficient quantity of material {material.Name}. Available: {material.Quantity}, Requested: {amount}.");
            }

            material.Quantity -= amount;
            _materialRepository.Update(material);
        }

        // Increase Material Quantity (Business Logic Example)
        public async Task IncreaseMaterialQuantityAsync(int materialId, int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Increase amount must be greater than zero.");
            }

            var material = await GetMaterialByIdAsync(materialId);
            material.Quantity += amount;

            _materialRepository.Update(material);
        }
    }*/
}
