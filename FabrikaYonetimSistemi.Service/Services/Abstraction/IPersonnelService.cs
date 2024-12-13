using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IPersonnelService
    {
        Task<Personnel> GetPersonnelByIdAsync(int id);
        Task<IEnumerable<Personnel>> GetAllPersonnelAsync();
        Task AddPersonnelAsync(Personnel personnel);
        void UpdatePersonnel(Personnel personnel);
        void DeletePersonnel(int id);
    }
}
