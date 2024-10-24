using UnityEngine;

public class FireScroll : Consumable
{
    public override string ToString()
    {
        return "FireScroll"; // representation
    }

    public FireScroll()
    {
        ID = 5;
        effect = 30; // Dice roll effect for damage amount
    }
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


