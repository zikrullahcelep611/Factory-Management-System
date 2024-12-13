using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IMaterialTransactionService
    {
        Task<MaterialTransaction> GetTransactionByIdAsync(int id);
        Task<IEnumerable<MaterialTransaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(MaterialTransaction transaction);
        void UpdateTransaction(MaterialTransaction transaction);
        Task DeleteTransactionAsync(int id);
        Task<IEnumerable<MaterialTransaction>> GetTransactionsByMaterialIdAsync(int materialId);
        Task<IEnumerable<MaterialTransaction>> GetTransactionsByPersonnelIdAsync(int personnelId);
    }
}
