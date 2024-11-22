using UnityEngine;

public class FireScroll : Consumable
{

    private static readonly int _id = 5;  // Unique ID for the FireScroll class
    public override int ID => _id;        // Implements the ID property in Item
    public override int effect { get; set; } = 10;

    private static readonly string _description = "This is fire scroll. This item can cause 1-10 random damage to monster";
    public override string description => _description;

    protected override string _dynamicText { get; set; }

    private Monster _monster;

    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    public void Cast() // Casts fire spell to damage monster
    {
        Debug.Log("Rolling Dice for Fire Spell...");
        int result = randomNumber(effect);
        _monster.cur_hp -= result;
        _dynamicText = $"Player gives {result} damage to monster!";
    }

    public override string ItemActionText()
    {
        return _dynamicText;
    }

}


