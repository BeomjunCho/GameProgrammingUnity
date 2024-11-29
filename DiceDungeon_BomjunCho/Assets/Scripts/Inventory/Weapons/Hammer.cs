using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] private Monster _monster;

    private static readonly int _id = 7;  // Unique ID for the Dagger class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is a hammer. It deals 100 damage to monsters and can also break through soft walls.";
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
    private int _maxDamage = 100;

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
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.LongSword], 3.0f);
        int result = maxDamage;
        _monster.cur_hp -= result;
        _dynamicText = $"You attacked the monster with Hammer for {result} damage.";

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
