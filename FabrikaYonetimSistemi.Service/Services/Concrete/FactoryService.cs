using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.EntityFrameworkCore;


namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class FactoryService : IFactoryService
    {
        private readonly IRepository<Factory> _factoryRepository;

        public FactoryService(IRepository<Factory> factoryRepository)
        {
            _factoryRepository = factoryRepository;
        }

        // Get a factory by ID
        public async Task<Factory> GetFactoryByIdAsync(int id)
        {
            return await _factoryRepository.GetByIdAsync(id);
        }

        // Get all factories
        public async Task<IEnumerable<Factory>> GetAllFactoriesAsync()
        {
            return await _factoryRepository.GetAllAsync();
        }

        // Add a new factory
        public async Task AddFactoryAsync(Factory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            await _factoryRepository.AddAsync(factory);
        }

        // Update an existing factory
        public void UpdateFactory(Factory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factoryRepository.Update(factory);
        }

        // Delete a factory by ID
        public async Task DeleteFactoryAsync(int id)
        {
            var factory = await _factoryRepository.GetByIdAsync(id);
            if (factory == null)
                throw new KeyNotFoundException($"Factory with ID {id} not found.");

            _factoryRepository.Delete(factory);
        }

        // Get all buildings associated with a factory by Factory ID
        public async Task<IEnumerable<Building>> GetBuildingsByFactoryIdAsync(int factoryId)
        {
            var factory = await _factoryRepository
                .GetAll(f => f.Buildings) // Buildings'i Include ediyoruz.
                .FirstOrDefaultAsync(f => f.Id == factoryId);

            if (factory == null)
                throw new KeyNotFoundException($"Factory with ID {factoryId} not found.");

            return factory.Buildings;
        }
    }
}
