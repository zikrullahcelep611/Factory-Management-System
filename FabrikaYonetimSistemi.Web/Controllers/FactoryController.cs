using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using FabrikaYonetimSistemi.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace FabrikaYonetimSistemi.Web.Controllers
{
    [Authorize(Roles = "Admin,Personel")]
    [Route("factory")]
    public class FactoryController : Controller
    {
        private readonly IFactoryService _factoryService;

        public FactoryController(IFactoryService factoryService)
        {
            _factoryService = factoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var factories = await _factoryService.GetAllFactoriesAsync();

            var factoryViewModels = factories.Select(factory => new FactoryViewModel
            {
                Id = factory.Id,
                Name = factory.Name,
                Location = factory.Location
            }).ToList();

            return View(factoryViewModels);
        }     

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View();
        }

        // Add a new factory
        [HttpPost("add")]
        public async Task<IActionResult> Add(Factory factory)
        {
            if (ModelState.IsValid)
            {
                await _factoryService.AddFactoryAsync(factory);
                return RedirectToAction(""); // Fabrikaları listeleyen bir sayfaya yönlendirin
            }
            return RedirectToAction("add");
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var factory = await _factoryService.GetFactoryByIdAsync(id);

            if (factory == null)
                return NotFound("Factory not found.");

            return View(factory);
        }

        // Update an existing factory
        [HttpPost("update/{id}")]
        public IActionResult Update(Factory factory)
        {
            if (!ModelState.IsValid)
                return View(factory); // Hataları tekrar formda göster

            try
            {
                _factoryService.UpdateFactory(factory);
                return RedirectToAction(""); // Güncellemeden sonra Index sayfasına yönlendir
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(factory); // Hata mesajını göster
            }
        }

        // Delete a factory
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _factoryService.DeleteFactoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Buildings/{factoryId}")]
        public async Task<IActionResult> ListBuildings(int factoryId)
        {           
            var buildings = await _factoryService.GetBuildingsByFactoryIdAsync(factoryId);

            return View(buildings);
        }
    }
}
