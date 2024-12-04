using UnityEngine;

/// <summary>
/// Represents a weapon, Dagger, that deals random damage to a monster.
/// Inherits from the Weapon base class.
/// </summary>
public class Dagger : Weapon
{
    [SerializeField] private Monster _monster;

    private static readonly int _id = 1;  // Unique ID for the Dagger class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is dagger. This weapon can cause 1-8 random damage to monster.";
    public override string description => _description;

    public override int maxDamage
    {
        get => _maxDamage;
        set
        {
            Debug.Log($"maxDamage is being set to {value}");
            _maxDamage = value;
        }
    }
    private int _maxDamage = 8;

    protected override string _dynamicText { get; set; }

    /// <summary>
    /// Assigns a target monster for the Dagger to attack.
    /// </summary>
    /// <param name="monster">The monster to be attacked by the Dagger.</param>
    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    /// <summary>
    /// Attacks the target monster, dealing random damage between 1 and the dagger's maximum damage.
    /// </summary>
    public override void Attack() // attack monster for random damage 
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.Dagger], 3.0f);
        int result = randomNumber(maxDamage);
        _monster.cur_hp -= result;
        _dynamicText = $"Dice number was {result}!\nYou attacked the monster with Dagger for {result} damage.";    
            
    }

    /// <summary>
    /// Provides a description of the action taken with the Dagger.
    /// </summary>
    /// <returns>A string describing the Dagger's attack effect.</returns>
    public override string ItemActionText()
    {
        return _dynamicText;
    }
}


