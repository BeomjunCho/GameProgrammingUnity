using UnityEngine;

public class FireScroll : Consumable
{
    private static readonly int _id = 5;  // Unique ID for the FireScroll class
    public override int ID => _id;        // Implements the ID property in Item
    public override int effect { get; set; }

    public void Cast(Monster monster) // Casts fire spell to damage monster
    {
        Debug.Log("Rolling Dice for Fire Spell...");
        int result = randomNumber(effect);
        Debug.Log($"Your dice number was {result}!");
          
        Debug.Log($"You cast a Fire Spell and dealt {result} damage to the monster!");
        monster.hp -= result;
            
    }
}


