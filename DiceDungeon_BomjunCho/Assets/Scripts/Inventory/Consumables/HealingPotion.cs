using UnityEngine;

public class HealingPotion : Consumable
{
    private static readonly int _id = 4;  // Unique ID for the Dagger class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is Healing potion. This item can cause 1-6 random heal to player.";
    public override string description => _description;

    public override int effect { get; set; } = 6;

    protected override string _dynamicText { get; set; }

    private Player _player;

    public void GetPlayer()
    {
        _player = Object.FindAnyObjectByType<Player>();
    }
    public void Heal() // heal _player until 10 curHP, without exceeding it
    {
        int result = randomNumber(effect);
        int potentialNewHp = _player.curHP + result; // Calculate the potential new curHP

        if (_player.curHP == 10) // when _player curHP is already full
        {
            _dynamicText = $"Your heal potion is enchanted by dice number {result}" +
                $"You are already fully healed. You don't get healed.";
        }
        else if (potentialNewHp >= 10) // if _player curHP is potentially over 10
        {
            int healAmount = 10 - _player.curHP; // calculate heal amount
            _player.curHP = 10; // set _player curHP to 10
            _dynamicText = $"Your heal potion is enchanted by dice number {result}." +
                $"You got healed for {healAmount} by using heal potion. Your curHP is now full at 10.";
        }
        else
        {
            _player.curHP += result; // add result to _player curHP
            _dynamicText = $"Your heal potion is enchanted by dice number {result}" +
                $"You got healed for {result} by using heal potion. Your current curHP is {_player.curHP}.";
        }
    }

    public override string ItemActionText()
    {
        return _dynamicText;    
    }
}


