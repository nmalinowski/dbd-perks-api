using DbdPerksApi.Models;
using DbdPerksApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DbdPerksApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerksController : ControllerBase
{
    private readonly IPerkService _perkService;

    public PerksController(IPerkService perkService)
    {
        _perkService = perkService;
    }

    /// <summary>
    /// Get all perks
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Perk>>> GetAllPerks()
    {
        try
        {
            var perks = await _perkService.GetAllPerksAsync();
            return Ok(perks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Get a specific perk by ID
    /// </summary>
    /// <param name="id">The ID of the perk</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<Perk>> GetPerk(int id)
    {
        try
        {
            var perk = await _perkService.GetPerkByIdAsync(id);
            if (perk == null)
            {
                return NotFound();
            }
            return Ok(perk);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Get perks by type (Survivor or Killer)
    /// </summary>
    /// <param name="type">The type of perk</param>
    [HttpGet("type/{type}")]
    public async Task<ActionResult<IEnumerable<Perk>>> GetPerksByType(PerkType type)
    {
        try
        {
            var perks = await _perkService.GetPerksByTypeAsync(type);
            return Ok(perks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Get perks by character type (Survivor or Killer)
    /// </summary>
    /// <param name="characterType">The character type</param>
    [HttpGet("character/{characterType}")]
    public async Task<ActionResult<IEnumerable<Perk>>> GetPerksByCharacterType(CharacterType characterType)
    {
        try
        {
            var perks = await _perkService.GetPerksByCharacterTypeAsync(characterType);
            return Ok(perks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
