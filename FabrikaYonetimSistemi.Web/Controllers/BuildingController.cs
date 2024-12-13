using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BuildingController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    // GET: api/Building
    [HttpGet]
    public async Task<IActionResult> GetAllBuildings()
    {
        var buildings = await _buildingService.GetAllBuildingsAsync();
        return Ok(buildings);
    }

    // GET: api/Building/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBuildingById(int id)
    {
        var building = await _buildingService.GetBuildingByIdAsync(id);
        if (building == null)
        {
            return NotFound($"Building with ID {id} not found.");
        }
        return Ok(building);
    }

    // GET: api/Building/factory/{factoryId}
    [HttpGet("factory/{factoryId}")]
    public async Task<IActionResult> GetBuildingsByFactoryId(int factoryId)
    {
        var buildings = await _buildingService.GetBuildingsByFactoryIdAsync(factoryId);
        return Ok(buildings);
    }

    // POST: api/Building
    [HttpPost]
    public async Task<IActionResult> AddBuilding([FromBody] Building building)
    {
        if (building == null)
        {
            return BadRequest("Building data is required.");
        }

        var isUnique = await _buildingService.IsBuildingNameUniqueAsync(building.Name);
        if (!isUnique)
        {
            return BadRequest($"A building with the name '{building.Name}' already exists.");
        }

        await _buildingService.AddBuildingAsync(building);
        return CreatedAtAction(nameof(GetBuildingById), new { id = building.Id }, building);
    }

    // PUT: api/Building/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateBuilding(int id, [FromBody] Building building)
    {
        if (building == null || building.Id != id)
        {
            return BadRequest("Invalid building data.");
        }

        _buildingService.UpdateBuilding(building);
        return NoContent();
    }

    // DELETE: api/Building/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuilding(int id)
    {
        var building = await _buildingService.GetBuildingByIdAsync(id);
        if (building == null)
        {
            return NotFound($"Building with ID {id} not found.");
        }

        _buildingService.DeleteBuilding(building);
        return NoContent();
    }
}
