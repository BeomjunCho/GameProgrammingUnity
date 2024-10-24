using UnityEngine;

public class LongSword : Weapon
{
    public override string ToString()
    {
        return "LongSword"; //representation
    }

    public LongSword()
    {
        ID = 3;
        maxDamage = 12;
    }
    public override int maxDamage { get; set; }

    public override void Attack(Monster monster) // attack monster for random damage 
    {
        Debug.Log("Rolling Dice...");
        int result = randomNumber(maxDamage);
        Debug.Log($"Your dice number was {result}!");
        monster.hp -= result;
            
        Debug.Log($"You attacked the monster with Long Sword for {result} damage.");
            
    }
}


