using UnityEngine;

public class DragonSword : Weapon
{
    public override string ToString()
    {
        return "DragonSword"; //representation
    }

    public DragonSword()
    {
        ID = 4;
        maxDamage = 20;
    }
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


