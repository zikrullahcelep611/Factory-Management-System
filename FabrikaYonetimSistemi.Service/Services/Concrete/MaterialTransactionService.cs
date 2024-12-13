using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class MaterialTransactionService : IMaterialTransactionService
    {
        private readonly IRepository<MaterialTransaction> _transactionRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<Personnel> _personnelRepository;

        public MaterialTransactionService(
            IRepository<MaterialTransaction> transactionRepository,
            IRepository<Material> materialRepository,
            IRepository<Personnel> personnelRepository)
        {
            _transactionRepository = transactionRepository;
            _materialRepository = materialRepository;
            _personnelRepository = personnelRepository;
        }

        // Get MaterialTransaction by ID
        public async Task<MaterialTransaction> GetTransactionByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                throw new Exception($"MaterialTransaction with ID {id} not found.");
            }
            return transaction;
        }

        // Get all MaterialTransactions
        public async Task<IEnumerable<MaterialTransaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        // Add a new MaterialTransaction
        public async Task AddTransactionAsync(MaterialTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            // Validate Material
            var material = await _materialRepository.GetByIdAsync(transaction.MaterialId);
            if (material == null)
            {
                throw new Exception($"Material with ID {transaction.MaterialId} not found.");
            }

            // Validate Personnel
            var personnel = await _personnelRepository.GetByIdAsync(transaction.PersonelId);
            if (personnel == null)
            {
                throw new Exception($"Personnel with ID {transaction.PersonelId} not found.");
            }

            // Update Material Quantity based on ActionType
            if (transaction.Action == ActionType.Add)
            {
                material.Quantity += transaction.Quantity;
            }
            else if (transaction.Action == ActionType.Remove)
            {
                if (material.Quantity < transaction.Quantity)
                {
                    throw new Exception("Insufficient material quantity for the transaction.");
                }
                material.Quantity -= transaction.Quantity;
            }

            // Save changes
            await _transactionRepository.AddAsync(transaction);
            _materialRepository.Update(material);
        }

        // Update an existing MaterialTransaction
        public void UpdateTransaction(MaterialTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            _transactionRepository.Update(transaction);
        }

        // Delete MaterialTransaction by ID
        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
            {
                throw new Exception($"MaterialTransaction with ID {id} not found.");
            }

            // Revert material quantity based on ActionType
            var material = await _materialRepository.GetByIdAsync(transaction.MaterialId);
            if (transaction.Action == ActionType.Remove)
            {
                material.Quantity -= transaction.Quantity;
            }
            else if (transaction.Action == ActionType.Add)
            {
                material.Quantity += transaction.Quantity;
            }

            // Save changes
            _transactionRepository.Delete(transaction);
            _materialRepository.Update(material);
        }

        // Get all transactions for a specific Material
        public async Task<IEnumerable<MaterialTransaction>> GetTransactionsByMaterialIdAsync(int materialId)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Where(t => t.MaterialId == materialId);
        }

        // Get all transactions for a specific Personnel
        public async Task<IEnumerable<MaterialTransaction>> GetTransactionsByPersonnelIdAsync(int personnelId)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Where(t => t.PersonelId == personnelId);
        }
    }
}
