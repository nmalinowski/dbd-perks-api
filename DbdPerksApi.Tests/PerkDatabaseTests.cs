using DbdPerksApi.Data;
using DbdPerksApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace DbdPerksApi.Tests;

public class PerkDatabaseTests
{
    [Fact]
    public void LoadPerks_ValidJson_ReturnsPerks()
    {
        var json = "[{\"Id\":1,\"Name\":\"Test\",\"Type\":0,\"CharacterType\":0,\"Description\":\"desc\",\"IconUrl\":\"/perk_icons/test.png\"}]";
        var perks = JsonSerializer.Deserialize<List<Perk>>(json);
        Assert.NotNull(perks);
        Assert.Single(perks);
        Assert.Equal("Test", perks[0].Name);
    }

    [Fact]
    public void LoadPerks_InvalidJson_ThrowsException()
    {
        var json = "[{'id':1,'name':'Test',"; // Malformed
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<List<Perk>>(json));
    }

    [Fact]
    public void LoadPerks_EmptyJson_ReturnsEmptyList()
    {
        var json = "[]";
        var perks = JsonSerializer.Deserialize<List<Perk>>(json);
        Assert.NotNull(perks);
        Assert.Empty(perks);
    }
}
