using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using FabrikaYonetimSistemi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabrikaYonetimSistemi.Web.Controllers
{
    [Authorize(Roles = "Admin,Personel")]
    [Route("Storage")]
    public class StorageController : Controller
    {
        private readonly IStorageService _storageService;
        private readonly IBuildingService _buildingService;

        public StorageController(IStorageService storageService, IBuildingService buildingService)
        {
            _storageService = storageService;
            _buildingService = buildingService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var storages = await _storageService.GetAllStoragesAsync();

            var storageViewModels = storages.Select(storages => new StorageViewModel
            {
                Id = storages.Id,
                Name = storages.Name,
                Building = storages.Building
            }).ToList();

            return View(storageViewModels);
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
            var buildings = await _buildingService.GetAllBuildingsAsync();

            ViewBag.Buildings = buildings;

            return View();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Storage storage)
        {
            if(storage == null)
            {
                ModelState.AddModelError("", "Storage data is requiered.");
                return View(storage);
            }

            await _storageService.AddStorageAsync(storage);
            return RedirectToAction("");
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var storage = await _storageService.GetStorageByIdAsync(id);

            if(storage == null)
            {
                return NotFound();
            }

            ViewBag.Buildings = await _buildingService.GetAllBuildingsAsync();
            return View(storage);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(Storage storage)
        {
            ModelState.Remove("Building");
            if (ModelState.IsValid)
            {
                _storageService.UpdateStorage(storage);
                return RedirectToAction("");
            }

            //ModalState hata logları
            else
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var errors = ModelState[modelStateKey].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
                ViewBag.Buildings = await _buildingService.GetAllBuildingsAsync();
                return View(storage);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storage = await _storageService.GetStorageByIdAsync(id);
            if (storage == null)
            {
                return NotFound($"Storage with ID {id} not found");
            }
            await _storageService.DeleteStorageAsync(storage);
            return NoContent();
        }
    }
}
