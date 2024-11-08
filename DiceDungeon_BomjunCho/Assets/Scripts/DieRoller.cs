using UnityEngine;

public class DieRoller : MonoBehaviour
{
    public void GamePlayLoop(Player user, Monster monster) // Main game play loop
    {
        while(monster.hp > 0 && user.hp > 0) // loop until monster hp or user hp is less than 0
        {
            int turnOrder = Random.Range(1, 3); // turn decided randomly every time

            if (turnOrder == 1) // First turn order
            {
                Debug.Log("\n[[[It is your turn!]]]");
                monster.ShowHp();
                //user.ShowPlayerState();
                //PickItemUse(user, monster); // user choose and use item

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
                    //user.ShowHp();
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
                    //user.ShowHp();

                }

                Debug.Log("\n[[[Now, it is your turn!]]]"); //user attack turn

                monster.ShowHp();
                //user.ShowPlayerState(); 
                //PickItemUse(user, monster); // user choose and use item
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

    /*
    public void PickItemUse(Player player, Monster monster) // Pick the item and use it
    {
        Debug.Log("\nPick the item(Enter number of item) \n-----1: Dagger, 2: Long sword, 3: Dragon Sword, 4: Healing Potion, 30: Fire Scroll, 6: Shield scroll-----");
        string userChoice = "8";

        if (userChoice == "1" || userChoice == "2" || userChoice == "3" || userChoice == "4" || userChoice == "5" || userChoice == "6") // Check if user choose right dice
        {
            if (userChoice == "8" && Inventory.InventoryInstance.DoesPlayerHave(1)) // check and use dagger
            {
                
                Debug.Log($"Dagger is linked to {userChoice} sides dice.");

                Inventory.InventoryInstance.UseItem(1, monster, player);
            }
            else if (userChoice == "12" && Inventory.InventoryInstance.DoesPlayerHave(2)) // check and use long sword
            {
                
                Debug.Log($"Long sword is linked to {userChoice} sides dice.");

                Inventory.InventoryInstance.UseItem(2, monster, player);
            }
            else if (userChoice == "20" && Inventory.InventoryInstance.DoesPlayerHave(3)) // check and use dragon sword
            {
                
                Debug.Log($"Dragon sword is linked to {userChoice} sides dice.");
                
                Inventory.InventoryInstance.UseItem(3, monster, player);
            }
            else if (userChoice == "6" && Inventory.InventoryInstance.DoesPlayerHave(4)) // check and use heal potion
            {
                
                Debug.Log($"Healing potion is linked to {userChoice} sides dice.");
                
                Inventory.InventoryInstance.UseItem(4, monster, player);
            }
            else if (userChoice == "30" && Inventory.InventoryInstance.DoesPlayerHave(5)) // check and use fire scroll
            {
                
                Debug.Log($"Fire scroll is linked to {userChoice} sides dice.");
                
                Inventory.InventoryInstance.UseItem(6, monster, player);
            }
            else if (userChoice == "25" && Inventory.InventoryInstance.DoesPlayerHave(6)) // check and use shield scroll
            {
                
                Debug.Log($"Shield scroll is linked to {userChoice} sides dice.");
                
                Inventory.InventoryInstance.UseItem(5, monster, player);
            }
            else // user selected item which doesn't exist in inventoryPrefab
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
    */
}

