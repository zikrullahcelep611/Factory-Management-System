using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

[Route("material")]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService materialService)
    {
        _materialService = materialService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllMaterials()
    {
        var materials = await _materialService.GetAllMaterialsAsync();
        return Ok(materials);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMaterialById(int id)
    {
        var material = await _materialService.GetMaterialByIdAsync(id);
        if (material == null)
        {
            return NotFound($"Material with ID {id} not found.");
        }
        return Ok(material);
    }


    [HttpPost]
    public async Task<IActionResult> AddMaterial([FromBody] Material material)
    {
        if (material == null)
        {
            return BadRequest("Material data is required.");
        }

        await _materialService.AddMaterialAsync(material);
        return CreatedAtAction(nameof(GetMaterialById), new { id = material.Id }, material);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateMaterial(int id, [FromBody] Material material)
    {
        if (material == null || material.Id != id)
        {
            return BadRequest("Invalid material data.");
        }

        _materialService.UpdateMaterial(material);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaterial(int id)
    {
        var material = await _materialService.GetMaterialByIdAsync(id);
        if (material == null)
        {
            return NotFound($"Material with ID {id} not found.");
        }

        await _materialService.DeleteMaterialAsync(id);
        return NoContent();
    }


    [HttpPatch("{id}/increase")]
    public async Task<IActionResult> IncreaseMaterialQuantity(int id, [FromQuery] int amount)
    {
        if (amount <= 0)
        {
            return BadRequest("Amount must be greater than zero.");
        }

        try
        {
            await _materialService.IncreaseMaterialQuantityAsync(id, amount);
            return Ok($"Material quantity increased by {amount}.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPatch("{id}/reduce")]
    public async Task<IActionResult> ReduceMaterialQuantity(int id, [FromQuery] int amount)
    {
        if (amount <= 0)
        {
            return BadRequest("Amount must be greater than zero.");
        }

        try
        {
            await _materialService.ReduceMaterialQuantityAsync(id, amount);
            return Ok($"Material quantity reduced by {amount}.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}