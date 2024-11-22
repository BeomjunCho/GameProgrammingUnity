using UnityEngine;

public class ShieldScroll : Consumable
{
    private static readonly int _id = 6;  // Unique ID for the Shield class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is shield scroll. This item can cause 1-10 random shield amount to player. Shield will protect player from damage.";
    public override string description => _description;

    protected override string _dynamicText { get; set; }
    public override int effect { get; set; } = 10;


    private Player _player;
    public void GetPlayer()
    {
        _player = Object.FindAnyObjectByType<Player>();
    }
    public void Cast() // Grants temporary defense boost
    {
        int result = randomNumber(effect);
        _player.shield += result; 
        _dynamicText = $"Player gets {result} amount of shield!";
    }

    public override string ItemActionText()
    {
        return _dynamicText;
    }
}


