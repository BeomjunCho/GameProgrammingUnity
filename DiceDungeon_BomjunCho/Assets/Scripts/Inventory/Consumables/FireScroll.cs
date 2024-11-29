using UnityEngine;

public class FireScroll : Consumable
{

    private static readonly int _id = 5;  // Unique ID for the FireScroll class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is fire scroll. This item can cause 1-30 random damage to monster";
    public override string description => _description;
    public override int effect { get; set; } = 30;

    protected override string _dynamicText { get; set; }

    private Monster _monster;

    /// <summary>
    /// Assigns a target monster for the Fire Scroll's effect.
    /// </summary>
    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    /// <summary>
    /// Casts the Fire Scroll to deal random damage to the assigned monster.
    /// </summary>
    public void Cast() // Casts fire spell to damage monster
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.FireScroll], 3.0f);
        int result = randomNumber(effect);
        Debug.Log(effect);
        _monster.cur_hp -= result;
        _dynamicText = $"Dice number was {result}!\nPlayer gives {result} damage to monster!";
    }

    /// <summary>
    /// Provides a description of the action taken with the Fire Scroll.
    /// </summary>
    /// <returns>A string describing the Fire Scroll's effect.</returns>
    public override string ItemActionText()
    {
        return _dynamicText;
    }

}


