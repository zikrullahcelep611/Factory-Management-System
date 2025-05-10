using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using FabrikaYonetimSistemi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin,Personel")]
[Route("Material")]
public class MaterialController : Controller
{
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService materialService)
    {
        _materialService = materialService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var materials = await _materialService.GetAllMaterialsAsync();
        var materialViewModel = materials.Select(material => new MaterialViewModel
        {
            Id = material.Id,
            Name = material.Name
        }).ToList();

        return View(materialViewModel);
    }

    [HttpGet("Add")]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add(Material material)
    {
        if (material == null)
        {
            return BadRequest("Material data is required.");
        }

        await _materialService.AddMaterialAsync(material);
        return RedirectToAction("");
    }

    [HttpGet("Update/{id}")]
    public async Task<IActionResult> Update(int id)
    {
        var material = await _materialService.GetMaterialByIdAsync(id);

        return View(material);
    }

    [HttpPost("Update/{id}")]
    public async Task<IActionResult> Update(Material material)
    {
        if (ModelState.IsValid)
        {
            _materialService.UpdateMaterial(material);
            return RedirectToAction("");
        }
        
        return View(material);
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var material = await _materialService.GetMaterialByIdAsync(id);

        await _materialService.DeleteMaterialAsync(material);
        return RedirectToAction("");
    }
}
    