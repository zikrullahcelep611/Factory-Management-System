using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class MaterialTransactionService : IMaterialTransactionService
    {
        private readonly IRepository<MaterialTransaction> _transactionRepository;
        private readonly IRepository<StorageMaterial> _storageMaterialRepository;
        private readonly UserManager<Personnel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor; // IHttpContextAccessor tanımlandı
        
        public MaterialTransactionService(
            IRepository<MaterialTransaction> transactionRepository,
            IRepository<StorageMaterial> storageMaterialRepository,
            UserManager<Personnel> userManager,
            IHttpContextAccessor httpContextAccessor) // IHttpContextAccessor parametre olarak alınıyor
        {
            _transactionRepository = transactionRepository;
            _storageMaterialRepository = storageMaterialRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor; // Değişkene atanıyor
        }

        public async Task AddTransactionAsync(MaterialTransaction transaction)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new Exception("Kullanıcı oturumu bulunamadı!");
            }

            transaction.PersonnelId = user.Id; // Login kullanıcıyı işlemlere bağlama

            // İşlemi ekle
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task<IEnumerable<MaterialTransaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync(t => t.Personnel, t => t.StorageMaterial, t => t.StorageMaterial.Material);
        }

        public async Task<MaterialTransaction> GetMaterialTransactionByIdAsync(int materialTransactionId)
        {
            var materialTransaction = await _transactionRepository.GetByIdAsync(materialTransactionId);

            return materialTransaction;
        }
    }
}
