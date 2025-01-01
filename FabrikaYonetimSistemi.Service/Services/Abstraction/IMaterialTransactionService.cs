using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IMaterialTransactionService
    {
        Task<IEnumerable<MaterialTransaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(MaterialTransaction transaction);
    }
}
