using DbdPerksApi.Models;
using DbdPerksApi.Data;

namespace DbdPerksApi.Services;

public class PerkService : IPerkService
{
    private readonly List<Perk> _perks;

    public PerkService()
    {
        // Initialize with all perks from the database
        _perks = PerkDatabase.GetAllPerks();
    }

    public Task<IEnumerable<Perk>> GetAllPerksAsync()
    {
        return Task.FromResult(_perks.AsEnumerable());
    }

    public Task<Perk?> GetPerkByIdAsync(int id)
    {
        var perk = _perks.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(perk);
    }

    public Task<IEnumerable<Perk>> GetPerksByTypeAsync(PerkType type)
    {
        var perks = _perks.Where(p => p.Type == type);
        return Task.FromResult(perks.AsEnumerable());
    }

    public Task<IEnumerable<Perk>> GetPerksByCharacterTypeAsync(CharacterType characterType)
    {
        var perks = _perks.Where(p => p.CharacterType == characterType);
        return Task.FromResult(perks.AsEnumerable());
    }
}
