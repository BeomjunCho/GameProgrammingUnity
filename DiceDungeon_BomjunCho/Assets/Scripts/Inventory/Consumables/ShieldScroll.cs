using UnityEngine;

/// <summary>
/// Represents a consumable item, Shield Scroll, that provides temporary shield to the player.
/// Shield protects the player from incoming damage.
/// Inherits from the Consumable base class.
/// </summary>
public class ShieldScroll : Consumable
{
    private static readonly int _id = 6;  // Unique ID for the Shield class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is shield scroll. This item can cause 1-20 random shield amount to player. Shield will protect player from damage.";
    public override string description => _description;

    protected override string _dynamicText { get; set; }
    public override int effect { get; set; } = 20;


    private Player _player;

    /// <summary>
    /// Finds and assigns the Player object in the scene.
    /// </summary>
    public void GetPlayer()
    {
        _player = Object.FindAnyObjectByType<Player>();
    }

    /// <summary>
    /// Grants the player a random amount of shield based on the scroll's effect.
    /// Shield absorbs incoming damage until it depletes.
    /// </summary>
    public void Cast() // Grants temporary defense boost
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.ShieldScroll], 3.0f);
        int result = randomNumber(effect);
        Debug.Log(effect);
        _player.shield += result; 
        _dynamicText = $"Dice number was {result}!\nPlayer gets {result} amount of shield!";
    }

    /// <summary>
    /// Provides a description of the action taken with the Shield Scroll.
    /// </summary>
    /// <returns>A string describing the Shield Scroll's effect.</returns>
    public override string ItemActionText()
    {
        return _dynamicText;
    }
}


