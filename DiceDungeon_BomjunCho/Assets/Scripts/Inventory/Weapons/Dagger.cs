using UnityEngine;

public class Dagger : Weapon
{
    public override string ToString()
    {
        return "Dagger"; //representation
    }
    public Dagger()
    {
        ID = 2;
        maxDamage = 8;
    }
    public override int maxDamage { get; set; }
    public override void Attack(Monster monster) // attack monster for random damage 
    {
        Debug.Log("Rolling Dice...");
        int result = randomNumber(maxDamage);
        Debug.Log($"Your dice number was {result}!");
        monster.hp -= result;
            
        Debug.Log($"You attacked the monster with Dagger for {result} damage.");
            
    }
}


