using UnityEngine;

public class DieRoller : MonoBehaviour
{

    Dagger dagger = new Dagger();
    LongSword longSword = new LongSword();
    DragonSword dragonSword = new DragonSword();
    HealingPotion healPotion = new HealingPotion();
    FireScroll fireScroll = new FireScroll();
    ShieldScroll shieldScroll = new ShieldScroll();

    public void GamePlayLoop(Player user, Monster monster) // Main game play loop
    {
        while(monster.hp > 0 && user.hp > 0) // loop until monster hp or user hp is less than 0
        {
            int turnOrder = Random.Range(1, 3); // turn decided randomly every time

            if (turnOrder == 1) // First turn order
            {
                Debug.Log("\n[[[It is your turn!]]]");
                monster.ShowHp();
                user.ShowPlayerState();
                PickItemUse(user, monster); // user choose and use item

                if (monster.hp <= 0) // check if monster is dead
                {
                    Debug.Log($"\nYou killed the {monster.ToString()}!");
                    break; 
                }
                else
                {
                    monster.ShowHp();
                }
                Debug.Log("\n[[[Now it is Monster turn!]]]");
                monster.Attack(user);
                    
                if (user.hp <= 0) // check if user is dead
                {
                    Debug.Log($"\n{monster.ToString()} killed you...\nRIP");
                    break;
                }
                else
                {
                    user.ShowHp();
                }
            }

            else // Second turn order
            {
                Debug.Log("\n[[[It is Monster turn!]]]");
                monster.Attack(user);

                if (user.hp <= 0)
                {
                    Debug.Log($"\n{monster.ToString()} killed you...\nRIP");
                    break;
                }
                else
                {
                    user.ShowHp();

                }

                Debug.Log("\n[[[Now, it is your turn!]]]"); //user attack turn

                monster.ShowHp();
                user.ShowPlayerState(); 
                PickItemUse(user, monster); // user choose and use item
                if (monster.hp <= 0)
                {
                    Debug.Log($"\nYou killed the {monster.ToString()}!");
                    break;
                }
                else
                {
                    monster.ShowHp();
                }
            }
        }
    }


    public void PickItemUse(Player player, Monster monster) // Pick the item and use it
    {
        Inventory playerInventory = player.inventory; // call inventory from player instance "user"
        Debug.Log("\nPick the item(Enter number of item) \n-----6: Healing Potion, 8: Dagger, 12: Long sword, 20: Dragon Sword, 25: Shield scroll 30: Fire Scroll-----");
        string userChoice = "8";

        if (userChoice == "6" || userChoice == "8" || userChoice == "12" || userChoice == "20" || userChoice == "25" || userChoice == "30") // Check if user choose right dice
        {
            if (userChoice == "6" && player.inventory.DoesPlayerHave(healPotion.ID)) // check and use heal potion
            {
                
                Debug.Log($"Healing potion is linked to {userChoice} sides dice.");
                
                player.inventory.UseItem(healPotion, monster, player);
            }
            else if (userChoice == "8" && player.inventory.DoesPlayerHave(dagger.ID)) // check and use dagger
            {
                
                Debug.Log($"Dagger is linked to {userChoice} sides dice.");
                
                player.inventory.UseItem(dagger, monster, player);
            }
            else if (userChoice == "12" && player.inventory.DoesPlayerHave(longSword.ID)) // check and use long sword
            {
                
                Debug.Log($"Long sword is linked to {userChoice} sides dice.");
                
                player.inventory.UseItem(longSword, monster, player);
            }
            else if (userChoice == "20" && player.inventory.DoesPlayerHave(dragonSword.ID)) // check and use dragon sword
            {
                
                Debug.Log($"Dragon sword is linked to {userChoice} sides dice.");
                
                player.inventory.UseItem(dragonSword, monster, player);
            }
            else if (userChoice == "25" && player.inventory.DoesPlayerHave(shieldScroll.ID)) // check and use shield scroll
            {
                
                Debug.Log($"Shield scroll is linked to {userChoice} sides dice.");
                
                player.inventory.UseItem(shieldScroll, monster, player);
            }
            else if (userChoice == "30" && player.inventory.DoesPlayerHave(fireScroll.ID)) // check and use fire scroll
            {
                
                Debug.Log($"Fire scroll is linked to {userChoice} sides dice.");
                
                player.inventory.UseItem(fireScroll, monster, player);
            }
            else // user selected item which doesn't exist in inventory
            {
                Debug.Log("You don't have the selected item. Pick another one.");
                
                PickItemUse(player, monster);
            }
        }
        else // user entered wrong text
        {
            Debug.Log("Entry is invalid. Try again");
            Debug.Log("No item selected.");
            
            PickItemUse(player, monster);
        }

    }
}

