using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IFactoryService
    {
        Task<Factory> GetFactoryByIdAsync(int id);
        Task<IEnumerable<Factory>> GetAllFactoriesAsync();
        Task AddFactoryAsync(Factory factory);
        void UpdateFactory(Factory factory);
        Task DeleteFactoryAsync(int id);
        Task<IEnumerable<Building>> GetBuildingsByFactoryIdAsync(int factoryId);
    }
}
