using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabrikaYonetimSistemi.Web.Controllers
{
    [Authorize(Roles = "Admin, Personel")]
    [Route("Transaction")]
    public class TransactionController : Controller
    {
        private readonly IMaterialService _materialService;
        private readonly IStorageService _storageService;
        private readonly IStorageMaterialService _storageMaterialService;
        private readonly IMaterialTransactionService _materialTransactionService;

        public TransactionController(IMaterialService materialService, IStorageService storageService, IStorageMaterialService storageMaterialService, IMaterialTransactionService materialTransactionService)
        {
            _materialService = materialService;
            _storageService = storageService;
            _storageMaterialService = storageMaterialService;
            _materialTransactionService = materialTransactionService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var transactions = await _materialTransactionService.GetAllTransactionsAsync();

            return View(transactions);
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add(int id)
        {
            var storageMaterial = await _storageMaterialService.GetStorageMaterialByIdAsync(id);
            
            
            return View(storageMaterial);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(int storageMaterialId, int quantity, int personnelId)
        {
            var storageMaterial = await _storageMaterialService.GetStorageMaterialByIdAsync(storageMaterialId);
            if(storageMaterial == null)
            {
                return NotFound("Material not found.");
            }

            storageMaterial.Quantity += quantity;

            await _storageMaterialService.UpdateStorageMaterialAsync(storageMaterial);

            var transaction = new MaterialTransaction
            {
                StorageMaterialId = storageMaterial.Id,
                QuantityChange = quantity,
                TransactionType = ActionType.Add,
                TransactionDate = DateTime.UtcNow,
                PersonnelId = personnelId
            };

            await _materialTransactionService.AddTransactionAsync(transaction);


            return RedirectToAction("");
        }

        [HttpPost("AddMaterial")]
        public async Task<IActionResult> AddMaterial(int materialId, int quantity)
        {
            // Kullanıcıya bağlan ve işlem oluştur
            var transaction = new MaterialTransaction
            {
                StorageMaterialId = materialId,
                QuantityChange = quantity,
                TransactionType = ActionType.Add,
                TransactionDate = DateTime.Now
            };

            await _materialTransactionService.AddTransactionAsync(transaction);

            return RedirectToAction("");
        }

        [HttpPost("RemoveMaterial")]
        public async Task<IActionResult> RemoveMaterial(int materialId, int quantity)
        {
            // Kullanıcıya bağlan ve işlem oluştur
            var transaction = new MaterialTransaction
            {
                StorageMaterialId = materialId,
                QuantityChange = quantity,
                TransactionType = ActionType.Remove,
                TransactionDate = DateTime.Now
            };

            await _materialTransactionService.AddTransactionAsync(transaction);

            return RedirectToAction("");
        }
    }
}
