using DbdPerksApi.Models;
using DbdPerksApi.Services;
using Xunit;

namespace DbdPerksApi.Tests;

public class PerkServiceTests
{
    private readonly IPerkService _perkService;

    public PerkServiceTests()
    {
        _perkService = new PerkService();
    }

    [Fact]
    public async Task GetAllPerks_Returns_All_Perks()
    {
        // Act
        var perks = await _perkService.GetAllPerksAsync();

        // Assert
        Assert.NotNull(perks);
        Assert.NotEmpty(perks);
        Assert.Equal(291, perks.Count()); // There are 291 perks in the database
    }

    [Fact]
    public async Task GetPerkById_Returns_Correct_Perk()
    {
        // Arrange
        var expectedId = 2;
        var expectedName = "Adrenaline";

        // Act
        var perk = await _perkService.GetPerkByIdAsync(expectedId);

        // Assert
        Assert.NotNull(perk);
        Assert.Equal(expectedId, perk.Id);
        Assert.Equal(expectedName, perk.Name);
    }

    [Fact]
    public async Task GetPerkById_Returns_Null_For_Invalid_Id()
    {
        // Arrange
        var invalidId = 999;

        // Act
        var perk = await _perkService.GetPerkByIdAsync(invalidId);

        // Assert
        Assert.Null(perk);
    }

    [Fact]
    public async Task GetPerksByType_Returns_Survivor_Perks()
    {
        // Act
        var perks = await _perkService.GetPerksByTypeAsync(PerkType.Survivor);

        // Assert
        Assert.NotNull(perks);
        Assert.NotEmpty(perks);
        Assert.All(perks, p => Assert.Equal(PerkType.Survivor, p.Type));
    }

    [Fact]
    public async Task GetPerksByCharacterType_Returns_Killer_Perks()
    {
        // Act
        var perks = await _perkService.GetPerksByCharacterTypeAsync(CharacterType.Killer);

        // Assert
        Assert.NotNull(perks);
        Assert.NotEmpty(perks);
        Assert.All(perks, p => Assert.Equal(CharacterType.Killer, p.CharacterType));
    }
}
