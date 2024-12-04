using UnityEngine;

public class ShieldScroll : Consumable
{
    private static readonly int _id = 6;  // Unique ID for the Shield class
    public override int ID => _id;        // Implements the ID property in Item
    public override int effect { get; set; }

    public void Cast(Player user) // Grants temporary defense boost
    {
        Debug.Log("Rolling Dice for Shield Spell...");
        int result = randomNumber(effect);
        Debug.Log($"Your dice number was {result}!");
          
        Debug.Log($"You cast a Shield Spell and gained {result} additional shield on your hp!");
        user.shield += result; 
    }
}


