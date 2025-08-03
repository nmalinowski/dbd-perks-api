using DbdPerksApi.Models;

namespace DbdPerksApi.Services;

public interface IPerkService
{
    Task<IEnumerable<Perk>> GetAllPerksAsync();
    Task<Perk?> GetPerkByIdAsync(int id);
    Task<IEnumerable<Perk>> GetPerksByTypeAsync(PerkType type);
    Task<IEnumerable<Perk>> GetPerksByCharacterTypeAsync(CharacterType characterType);
}
