using UnityEngine;

public class DragonSword : Weapon
{

    private static readonly int _id = 3;  // Unique ID for the DragonSword class
    public override int ID => _id;        // Implements the ID property in Item
    public override int maxDamage { get; set; }

    public override void Attack(Monster monster) // attack monster for random damage 
    {
        Debug.Log("Rolling Dice...");
        int result = randomNumber(maxDamage);
        Debug.Log($"Your dice number was {result}!");
        monster.hp -= result;
            
        Debug.Log($"You attacked the monster with Dragon Sword for {result} damage.");
            
    }
}


