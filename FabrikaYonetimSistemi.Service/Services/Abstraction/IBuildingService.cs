using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetBuildingsByFactoryIdAsync(int factoryId);
        Task<bool> IsBuildingNameUniqueAsync(string buildingName);

        Task<Building> GetBuildingByIdAsync(int id);
        Task<IEnumerable<Building>> GetAllBuildingsAsync();
        Task AddBuildingAsync(Building entity);
        void UpdateBuilding(Building entity);
        void DeleteBuilding(Building entity);
        Task<IEnumerable<Storage>> GetStoragesByBuildingIdAsync(int buildingId);
        Task<Building> GetBuildingByIdAndFactoryIdAsync(int buildingId, int factoryId);
    }
}
