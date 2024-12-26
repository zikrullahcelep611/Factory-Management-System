using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class BuildingService : IBuildingService
    {
        private readonly IRepository<Building> _buildingRepository;

        public BuildingService(IRepository<Building> repository)
        {
            _buildingRepository = repository;
        }

        public async Task AddBuildingAsync(Building entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _buildingRepository.AddAsync(entity);
        }

        public void DeleteBuilding(Building entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _buildingRepository.Delete(entity);
        }

        public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
        {
            return await _buildingRepository.GetAllAsync(b => b.Factory);
        }

        public async Task<Building> GetBuildingByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentException("ID must be greater than 0.", nameof(id));

            return await _buildingRepository.GetByIdAsync(id, b => b.Factory);
        }

        public async Task<IEnumerable<Building>> GetBuildingsByFactoryIdAsync(int factoryId)
        {
            if (factoryId <= 0)
                throw new ArgumentException("Factory ID must be greater than 0.", nameof(factoryId));

            var allBuildings = await _buildingRepository.GetAllAsync();
            return allBuildings.Where(b => b.FactoryId == factoryId);
        }

        public async Task<bool> IsBuildingNameUniqueAsync(string buildingName)
        {
            if (string.IsNullOrWhiteSpace(buildingName))
                throw new ArgumentException("Building name cannot be null or whitespace.", nameof(buildingName));

            var allBuildings = await _buildingRepository.GetAllAsync();
            return !allBuildings.Any(b => b.Name.Equals(buildingName, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateBuilding(Building entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _buildingRepository.Update(entity);
        }

        public async Task<IEnumerable<Storage>> GetStoragesByBuildingIdAsync(int buildingId)
        {
            var building = await _buildingRepository.GetAll(b => b.Storages)
                .FirstOrDefaultAsync(f => f.Id == buildingId);

            if(building == null)
            {
                throw new KeyNotFoundException($"Building with ID {buildingId} not found.");
            }

            return building.Storages;
        }
    }
}
