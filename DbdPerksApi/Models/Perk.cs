namespace DbdPerksApi.Models;

/// <summary>
/// Represents a perk in Dead by Daylight
/// </summary>
public class Perk
{
    /// <summary>
    /// Gets or sets the unique identifier for the perk
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the perk
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the perk
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the URL to the perk's icon
    /// </summary>
    public string IconUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of the perk (Survivor or Killer)
    /// </summary>
    public PerkType Type { get; set; }
    
    /// <summary>
    /// Gets or sets the character type associated with the perk (Survivor or Killer)
    /// </summary>
    public CharacterType CharacterType { get; set; }
    
    /// <summary>
    /// Gets or sets the category of the perk (for filtering)
    /// </summary>
    public string? Category { get; set; }
}

/// <summary>
/// Represents the type of perk
/// </summary>
public enum PerkType
{
    /// <summary>
    /// Survivor perk
    /// </summary>
    Survivor,
    
    /// <summary>
    /// Killer perk
    /// </summary>
    Killer
}

/// <summary>
/// Represents the character type
/// </summary>
public enum CharacterType
{
    /// <summary>
    /// Survivor character
    /// </summary>
    Survivor,
    
    /// <summary>
    /// Killer character
    /// </summary>
    Killer
}
