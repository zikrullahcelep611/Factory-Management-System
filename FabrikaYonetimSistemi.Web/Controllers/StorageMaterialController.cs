using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabrikaYonetimSistemi.Web.Controllers
{
    [Authorize(Roles ="Admin, Personel")]
    [Route("StorageMaterial")]
    public class StorageMaterialController : Controller
    {
        private readonly IStorageMaterialService _storageMaterialService;
        private readonly IMaterialService _materialService;
        private readonly IStorageService _storageService;
        private readonly IMaterialTransactionService _materialTransactionService;

        public StorageMaterialController(IMaterialTransactionService materialTransactionService,IStorageMaterialService storageMaterialService, IMaterialService materialService, IStorageService storageService)
        {
            _storageMaterialService = storageMaterialService;
            _materialService = materialService;
            _storageService = storageService;
            _materialTransactionService = materialTransactionService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var storageMaterials = await _storageMaterialService.GetAllStorageMaterialAsync();

            return View(storageMaterials);
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
            var allMaterials = await _materialService.GetAllMaterialsAsync();
            var allStorages = await _storageService.GetAllStoragesAsync();
            var storageMaterials = await _storageMaterialService.GetAllStorageMaterialAsync();

            // Henüz tüm depolara eklenmemiş malzemeleri filtrele
            var filteredMaterials = allMaterials.Where(material =>
                !storageMaterials.Any(sm => sm.MaterialId == material.Id && allStorages.All(storage => sm.StorageId == storage.Id))
            ).ToList();

            ViewBag.Materials = filteredMaterials;
            ViewBag.Storages = allStorages;

            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(StorageMaterial storageMaterial)
        {
            if (storageMaterial == null)
            {
                ModelState.AddModelError("", "Storage Material data is required.");
                return View(storageMaterial);
            }

            // Depoya eklenmiş malzemeleri kontrol et
            var existingStorageMaterial = await _storageMaterialService.GetByMaterialAndStorageAsync(storageMaterial.MaterialId, storageMaterial.StorageId);

            // Aynı depoya eklemeye izin verme
            if (existingStorageMaterial != null)
            {
                ModelState.AddModelError("", "Bu malzeme zaten seçilen depoya eklenmiş.");
                return View(storageMaterial);
            }

            // Malzemenin tüm depolara eklenmiş olup olmadığını kontrol et
            var allStorages = await _storageService.GetAllStoragesAsync();
            var materialInStorages = await _storageMaterialService.GetAllStoragesWithMaterialAsync(storageMaterial.MaterialId);

            if (allStorages.Count() == materialInStorages.Count())
            {
                ModelState.AddModelError("", "Bu malzeme zaten tüm depolara eklenmiş.");
                return RedirectToAction("Add");
            }

            // Yeni malzemeyi depoya ekle
            await _storageMaterialService.AddStorageMaterialAsync(storageMaterial);

            // Transaction kaydı oluştur
            var transaction = new MaterialTransaction
            {
                StorageMaterialId = storageMaterial.Id,
                QuantityChange = storageMaterial.Quantity,
                TransactionType = ActionType.Add,
                TransactionDate = DateTime.UtcNow,
                PersonnelId = 0 // Kullanıcı kimliğini bağlayabilirsiniz
            };
            await _materialTransactionService.AddTransactionAsync(transaction);
            return RedirectToAction("");
        }

        [HttpGet("AddMaterial/{materialId}")]
        public async Task<IActionResult> AddMaterial(int materialId)
        {
            var material = await _materialService.GetMaterialByIdAsync(materialId);

            if (material == null)
                return NotFound("Malzeme bulunamadı");

            var storages = await _storageService.GetAllStoragesAsync();

            ViewBag.Material = material;
            ViewBag.Storages = storages;

            return View();
        }

        [HttpPost("AddMaterial/{materialId}")]
        public async Task<IActionResult> AddMaterial(int materialId, int storageId, int quantity)
        {
            if(quantity <= 0)
            {
                ModelState.AddModelError("", "Miktar sıfırdan büyük olmalıdır.");
                return RedirectToAction("AddMaterial", new { materialId });
            }

            var existingStorageMaterial = await _storageMaterialService.GetByMaterialAndStorageAsync(materialId, storageId);

            if(existingStorageMaterial != null)
            {
                ModelState.AddModelError("", "Bu malzeme zaten seçilen depoya eklenmiş.");
                return RedirectToAction("AddMaterial", new { materialId });
            }

            // Yeni malzemeyi depoya ekle
            var storageMaterial = new StorageMaterial
            {
                MaterialId = materialId,
                StorageId = storageId,
                Quantity = quantity
            };

            await _storageMaterialService.AddStorageMaterialAsync(storageMaterial);

            // Transaction kaydı oluştur
            var transaction = new MaterialTransaction
            {
                StorageMaterialId = storageMaterial.Id,
                QuantityChange = quantity,
                TransactionType = ActionType.Add,
                TransactionDate = DateTime.UtcNow,
                PersonnelId = 0 // Kullanıcı kimliğini bağlayabilirsiniz
            };

            await _materialTransactionService.AddTransactionAsync(transaction);

            return RedirectToAction("");
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(int materialId, int quantity, string transactionType, string returnUrl)
        {
            if (quantity <= 0)
            {
                ModelState.AddModelError("", "Miktar sıfırdan büyük olmalıdır.");
                return Redirect(returnUrl ?? Url.Action(""));
            }

            var storageMaterial = await _storageMaterialService.GetStorageMaterialByIdAsync(materialId);

            if (storageMaterial == null)
            {
                ModelState.AddModelError("", "Storage Material data is requiered");
                return Redirect(returnUrl ?? Url.Action(""));
            }

            // İşlem türüne göre miktar artırma veya azaltma
            if (transactionType == "Add")
            {
                storageMaterial.Quantity += quantity;
            }
            else if (transactionType == "Remove")
            {
                if (storageMaterial.Quantity < quantity)
                {
                    ModelState.AddModelError("", "Yeterli miktar bulunmamaktadır.");
                    return Redirect(returnUrl ?? Url.Action(""));
                }
                storageMaterial.Quantity -= quantity;
            }
            else
            {
                ModelState.AddModelError("", "Geçersiz işlem türü.");
                return Redirect(returnUrl ?? Url.Action(""));
            }

            // StorageMaterial güncellemesi
            await _storageMaterialService.UpdateStorageMaterialAsync(storageMaterial);

            // İşlem kaydı oluşturma
            var transaction = new MaterialTransaction
            {
                StorageMaterialId = storageMaterial.Id,
                QuantityChange = quantity, // İşlem miktarını doğru şekilde atıyoruz
                TransactionType = transactionType == "Add" ? ActionType.Add : ActionType.Remove,
                TransactionDate = DateTime.UtcNow,
                PersonnelId = 0 // Kullanıcı kimliği burada bağlanmalı
            };
            await _materialTransactionService.AddTransactionAsync(transaction);

            return Redirect(returnUrl ?? Url.Action(""));
        }

        [HttpGet("MaterialList/{storageId}")]
        public async Task<IActionResult> ListMaterialsInStorage(int storageId)
        {
            var storageMaterials = await _storageMaterialService.GetStorageMaterialsByStorageIdAsync(storageId);

            if(storageMaterials == null)
            {
                return NotFound("Bu depoda malzeme bulunamadı.");
            }

            return View(storageMaterials);
        }
    }
}
