using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class BuildingService : IBuildingService
    {

        private readonly IRepository<Building> _repository;

        public BuildingService(IRepository<Building> repository)
        {
            _repository = repository;
        }

        public async Task AddBuildingAsync(Building entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _repository.AddAsync(entity);
        }

        public void DeleteBuilding(Building entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Delete(entity);
        }

        public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Building> GetBuildingByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentException("ID must be greater than 0.", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Building>> GetBuildingsByFactoryIdAsync(int factoryId)
        {
            if (factoryId <= 0)
                throw new ArgumentException("Factory ID must be greater than 0.", nameof(factoryId));

            var allBuildings = await _repository.GetAllAsync();
            return allBuildings.Where(b => b.FactoryId == factoryId);
        }

        public async Task<bool> IsBuildingNameUniqueAsync(string buildingName)
        {
            if (string.IsNullOrWhiteSpace(buildingName))
                throw new ArgumentException("Building name cannot be null or whitespace.", nameof(buildingName));

            var allBuildings = await _repository.GetAllAsync();
            return !allBuildings.Any(b => b.Name.Equals(buildingName, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateBuilding(Building entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _repository.Update(entity);
        }
    }
}
