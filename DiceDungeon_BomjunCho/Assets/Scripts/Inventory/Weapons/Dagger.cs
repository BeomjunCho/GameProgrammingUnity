using UnityEngine;

public class Dagger : Weapon
{
    private static readonly int _id = 1;  // Unique ID for the Dagger class
    public override int ID => _id;        // Implements the ID property in Item
    public override int maxDamage { get; set; } = 8;

    public override void Attack(Monster monster) // attack monster for random damage 
    {
        Debug.Log("Rolling Dice...");
        int result = randomNumber(maxDamage);
        Debug.Log($"Your dice number was {result}!");
        monster.hp -= result;
            
        Debug.Log($"You attacked the monster with Dagger for {result} damage.");
            
    }
}


