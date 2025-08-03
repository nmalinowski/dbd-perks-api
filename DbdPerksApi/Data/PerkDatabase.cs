using DbdPerksApi.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System;

namespace DbdPerksApi.Data;

/// <summary>
/// Provides a database of all Dead by Daylight perks
/// </summary>
public static class PerkDatabase
{
    /// <summary>
    /// Gets all perks in the database
    /// </summary>
    public static List<Perk> GetAllPerks()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "perks.json");
        Console.WriteLine($"Looking for perks.json at: {path}");

        if (!File.Exists(path))
        {
            Console.WriteLine("perks.json file not found!");
            return new List<Perk>();
        }

        var json = File.ReadAllText(path);
        Console.WriteLine($"Read {json.Length} characters from perks.json");

        // Deserialize the JSON as a dictionary
        var perksDictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
        if (perksDictionary == null)
        {
            Console.WriteLine("Failed to deserialize perks.json");
            return new List<Perk>();
        }

        Console.WriteLine($"Deserialized {perksDictionary.Count} perks from JSON");

        var perks = new List<Perk>();
        foreach (var kvp in perksDictionary)
        {
            var key = kvp.Key;
            var element = kvp.Value;

            try
            {
                var name = element.GetProperty("name").GetString() ?? "";
                var description = element.GetProperty("description").GetString() ?? "";
                var role = element.GetProperty("role").GetString() ?? "";
                var image = element.GetProperty("image").GetString() ?? "";

                var perkType = role.Equals("killer", StringComparison.OrdinalIgnoreCase)
                    ? PerkType.Killer
                    : PerkType.Survivor;

                var characterType = role.Equals("killer", StringComparison.OrdinalIgnoreCase)
                    ? CharacterType.Killer
                    : CharacterType.Survivor;

                string? category = null;
                if (
                    element.TryGetProperty("categories", out var categoriesProp)
                    && categoriesProp.ValueKind == JsonValueKind.Array
                    && categoriesProp.GetArrayLength() > 0
                )
                {
                    category = categoriesProp[0].GetString();
                }

                Console.WriteLine(
                    $"Processing perk: {name} (Role: {role}, PerkType: {perkType}, CharacterType: {characterType})"
                );

                perks.Add(
                    new Perk
                    {
                        Id = perks.Count + 1, // Auto-increment ID
                        Name = name,
                        Description = description,
                        IconUrl = image,
                        CharacterType = characterType,
                        Type = perkType,
                        Category = category
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing perk {key}: {ex.Message}");
            }
        }

        Console.WriteLine($"Returning {perks.Count} perks");
        return perks;
    }
}
