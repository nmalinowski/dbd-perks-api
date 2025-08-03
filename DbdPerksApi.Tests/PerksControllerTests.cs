using DbdPerksApi.Controllers;
using DbdPerksApi.Models;
using DbdPerksApi.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbdPerksApi.Tests;

public class PerksControllerTests
{
    private readonly PerksController _controller;
    private readonly Mock<IPerkService> _perkServiceMock = new();

    public PerksControllerTests()
    {
        _controller = new PerksController(_perkServiceMock.Object);
    }

    [Fact]
    public async Task GetAllPerks_Returns_Ok_With_Perks()
    {
        _perkServiceMock
            .Setup(s => s.GetAllPerksAsync())
            .ReturnsAsync(
                new List<Perk>
                {
                    new Perk { Id = 1, Name = "Test" }
                }
            );
        var result = await _controller.GetAllPerks();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var perks = Assert.IsAssignableFrom<IEnumerable<Perk>>(okResult.Value);
        Assert.Single(perks);
    }

    [Fact]
    public async Task GetPerk_Returns_Ok_When_Found()
    {
        _perkServiceMock
            .Setup(s => s.GetPerkByIdAsync(1))
            .ReturnsAsync(new Perk { Id = 1, Name = "Test" });
        var result = await _controller.GetPerk(1);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var perk = Assert.IsType<Perk>(okResult.Value);
        Assert.Equal(1, perk.Id);
    }

    [Fact]
    public async Task GetPerk_Returns_NotFound_When_Missing()
    {
        _perkServiceMock.Setup(s => s.GetPerkByIdAsync(99)).ReturnsAsync((Perk?)null);
        var result = await _controller.GetPerk(99);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetAllPerks_Returns_Ok_With_EmptyList()
    {
        _perkServiceMock.Setup(s => s.GetAllPerksAsync()).ReturnsAsync(new List<Perk>());
        var result = await _controller.GetAllPerks();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var perks = Assert.IsAssignableFrom<IEnumerable<Perk>>(okResult.Value);
        Assert.Empty(perks);
    }

    [Fact]
    public async Task GetPerk_Handles_Service_Exception()
    {
        _perkServiceMock
            .Setup(s => s.GetPerkByIdAsync(It.IsAny<int>()))
            .ThrowsAsync(new System.Exception("Service error"));
        var result = await _controller.GetPerk(1);
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, objectResult.StatusCode);
    }

    [Fact]
    public async Task GetPerksByType_Returns_Ok()
    {
        _perkServiceMock
            .Setup(s => s.GetPerksByTypeAsync(PerkType.Survivor))
            .ReturnsAsync(
                new List<Perk>
                {
                    new Perk { Id = 1, Type = PerkType.Survivor }
                }
            );
        var result = await _controller.GetPerksByType(PerkType.Survivor);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var perks = Assert.IsAssignableFrom<IEnumerable<Perk>>(okResult.Value);
        Assert.Single(perks);
    }

    [Fact]
    public async Task GetPerksByType_Handles_Service_Exception()
    {
        _perkServiceMock
            .Setup(s => s.GetPerksByTypeAsync(It.IsAny<PerkType>()))
            .ThrowsAsync(new System.Exception("Service error"));
        var result = await _controller.GetPerksByType(PerkType.Survivor);
        var objectResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, objectResult.StatusCode);
    }

    [Fact]
    public async Task GetPerksByCharacterType_Returns_Ok()
    {
        _perkServiceMock
            .Setup(s => s.GetPerksByCharacterTypeAsync(CharacterType.Killer))
            .ReturnsAsync(
                new List<Perk>
                {
                    new Perk { Id = 1, CharacterType = CharacterType.Killer }
                }
            );
        var result = await _controller.GetPerksByCharacterType(CharacterType.Killer);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var perks = Assert.IsAssignableFrom<IEnumerable<Perk>>(okResult.Value);
        Assert.Single(perks);
    }
}
