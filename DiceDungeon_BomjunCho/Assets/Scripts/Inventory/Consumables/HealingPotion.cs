using UnityEngine;

public class HealingPotion : Consumable
{
    public override string ToString()
    {
        return "HealingPotion"; //representation
    }
    public HealingPotion()
    {
        ID = 1;
        effect = 6;
    }
    public override int effect { get; set; }
    public void Heal(Player user) // heal user until 10 hp, without exceeding it
    {
        Debug.Log("Rolling Dice...");
        int result = randomNumber(effect);
        Debug.Log($"Your dice number was {result}!");  

        int potentialNewHp = user.hp + result; // Calculate the potential new hp

        if (user.hp == 10) // when user hp is already full
        {
            Debug.Log($"Your heal potion is enchanted by dice number {result}");
            Debug.Log("You are already fully healed. You don't get healed.");
        }
        else if (potentialNewHp >= 10) // if user hp is potentially over 10
        {
            int healAmount = 10 - user.hp; // calculate heal amount
            user.hp = 10; // set user hp to 10
            Debug.Log($"Your heal potion is enchanted by dice number {result}");
            Debug.Log($"You got healed for {healAmount} by using heal potion. Your hp is now full at 10.");
        }
        else
        {
            user.hp += result; // add result to user hp
            Debug.Log($"Your heal potion is enchanted by dice number {result}");
            Debug.Log($"You got healed for {result} by using heal potion. Your current hp is {user.hp}.");
        }
    }
}


