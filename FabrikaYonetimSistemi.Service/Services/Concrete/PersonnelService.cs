using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IRepository<Personnel> _repository;

        public PersonnelService(IRepository<Personnel> repository)
        {
            _repository = repository;
        }

        public async Task<Personnel> GetPersonnelByIdAsync(int id)
        {
            var personnel = await _repository.GetByIdAsync(id);

            if(personnel == null)
            {
                throw new Exception($"Personnel with ID {id} not found");
            }

            return personnel;
        }

        public async Task<IEnumerable<Personnel>> GetAllPersonnelAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddPersonnelAsync(Personnel personnel)
        {
            if(personnel == null)
            {
                throw new ArgumentNullException(nameof(personnel));
            }

            personnel.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(personnel);
        }

        public void UpdatePersonnel(Personnel personnel)
        {
            if (personnel == null)
            {
                throw new ArgumentNullException(nameof(personnel));
            }

            _repository.Update(personnel);
        }

        public void DeletePersonnel(int id)
        {
            var personnel = _repository.GetByIdAsync(id).Result;
            if (personnel == null)
            {
                throw new Exception($"Personnel with ID {id} not found.");
            }

            _repository.Delete(personnel);
        }
    }
}
