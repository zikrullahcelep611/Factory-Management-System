using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using FabrikaYonetimSistemi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin,Personel")]
[Route("Building")]
public class BuildingController : Controller
{
    private readonly IBuildingService _buildingService;
    private readonly IFactoryService _factoryService;

    public BuildingController(IBuildingService buildingService, IFactoryService factoryService)
    {
        _buildingService = buildingService;
        _factoryService = factoryService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var buildings = await _buildingService.GetAllBuildingsAsync();

        var buildingViewModels = buildings.Select(building => new BuildingViewModel
        {
            Id = building.Id,
            Name = building.Name,
            Factory = building.Factory
        }).ToList();

        return View(buildingViewModels);
    }

    [HttpGet("Add")]
    public async Task<IActionResult> Add()
    {
        var factories = await _factoryService.GetAllFactoriesAsync();

        ViewBag.Factories = factories;

        return View();
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(Building building)
    {
        if (building == null)
        {
            ModelState.AddModelError("", "Building data is required.");
            return View(building);
        }

        var isUnique = await _buildingService.IsBuildingNameUniqueAsync(building.Name);
        if (!isUnique)
        {
            ModelState.AddModelError("", $"A building with the name '{building.Name}' already exists.");
            return View(building);
        }

        await _buildingService.AddBuildingAsync(building);
        return RedirectToAction("");
    }

    [HttpGet("Update/{id}")]
    public async Task<IActionResult> Update(int id)
    {
        var building = await _buildingService.GetBuildingByIdAsync(id);

        if (building == null)
            return NotFound();
        ViewBag.Factories = await _factoryService.GetAllFactoriesAsync();

        return View(building);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> Update(Building building)
    {
        ModelState.Remove("Factory");
        if (ModelState.IsValid)
        {
            _buildingService.UpdateBuilding(building);
            return RedirectToAction("");
        }

        ViewBag.Factories = await _factoryService.GetAllFactoriesAsync();
        return View(building);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var building = await _buildingService.GetBuildingByIdAsync(id);
        if (building == null)
        {
            return NotFound($"Building with ID {id} not found.");
        }

        _buildingService.DeleteBuilding(building);
        return NoContent();
    }

    [HttpGet("Storages/{buildingId}")]
    public async Task<IActionResult> ListStorages(int buildingId)
    {
        try
        {
            var storages = await _buildingService.GetStoragesByBuildingIdAsync(buildingId);

            if (storages == null || !storages.Any())
                return NotFound($"No storages found for Building Id {buildingId}");

            return View(storages);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
