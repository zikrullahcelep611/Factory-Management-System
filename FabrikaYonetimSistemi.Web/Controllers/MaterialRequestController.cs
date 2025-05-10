using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FabrikaYonetimSistemi.Web.Controllers
{
    [Route("MaterialRequest")]
    public class MaterialRequestController : Controller
    {
        private readonly IMaterialRequestService _materialRequestService;
        private readonly IStorageMaterialService _storageMaterialService;
        private readonly UserManager<Personnel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMaterialTransactionService _materialTransactionService;

        public MaterialRequestController(IMaterialTransactionService materialTransactionService,IMaterialRequestService materialRequestService, IStorageMaterialService storageMaterialService
            , UserManager<Personnel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _materialRequestService = materialRequestService;
            _storageMaterialService = storageMaterialService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _materialTransactionService = materialTransactionService;
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var storageMaterials = await _storageMaterialService.GetAllStorageMaterialAsync();
            return View(storageMaterials);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(int StorageMaterialId, int Quantity)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (Quantity <= 0)
            {
                // Hatalı miktar durumunda geri dön
                TempData["Error"] = "Lütfen geçerli bir miktar giriniz.";
                return RedirectToAction("Index", "Storage"); // Depo sayfasına geri yönlendirin
            }

            var materialRequest = new MaterialRequest
            {
                StorageMaterialId = StorageMaterialId,
                Quantity = Quantity,
                PersonnelId = user.Id,
                RequestTime = DateTime.Now,
                Status = RequestStatus.Pending // Enum değeriniz bu şekilde olabilir
            };

            await _materialRequestService.CreateRequestAsync(materialRequest);
            
            TempData["Success"] = "Talebiniz başarıyla oluşturuldu.";
            return RedirectToAction("Index", "Storage"); // Talep oluşturduktan sonra depolara geri yönlendirin
        }

        // Personelin kendi taleplerini görmesi
        [HttpGet("MyRequests")]
        public async Task<IActionResult> MyRequests()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var personnelId = user.Id;
            var requests = (await _materialRequestService.GetAllRequestsAsync())
                            .Where(r => r.PersonnelId == personnelId);
            return View(requests);
        }

        // Admin için tüm talepleri listeleme
        [Authorize(Roles = "Admin")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var requests = await _materialRequestService.GetAllRequestsAsync();
            return View(requests);
        }

        // Admin için talebi onaylama
        [Authorize(Roles = "Admin")]
        [HttpPost("Approve")]
        public async Task<IActionResult> Approve(int id)
        {
            var materialRequest = await _materialRequestService.GetRequestByIdAsync(id);
            
            await _materialRequestService.ApproveRequestAsync(id);
            
            var materialTransaction = new MaterialTransaction
            {
                StorageMaterialId = materialRequest.StorageMaterialId,
                QuantityChange = materialRequest.Quantity,
                PersonnelId = materialRequest.PersonnelId,
                TransactionDate = DateTime.Now,
                TransactionType = ActionType.Add
            };
            
            await _materialTransactionService.AddTransactionAsync(materialTransaction);
            return RedirectToAction("Index");
        }

        // Admin için talebi reddetme
        [Authorize(Roles = "Admin")]
        [HttpPost("Reject")]
        public async Task<IActionResult> Reject(int id)
        {
            await _materialRequestService.RejectRequestAsync(id);
            return RedirectToAction("Index");
        }
    }
}
