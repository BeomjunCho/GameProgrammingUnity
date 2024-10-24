using UnityEngine;

public class ShieldScroll : Consumable
{
    public override string ToString()
    {
        return "ShieldScroll"; // representation
    }

    public ShieldScroll()
    {
        ID = 6;
        effect = 25; // Dice roll effect for defense boost amount
    }
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


