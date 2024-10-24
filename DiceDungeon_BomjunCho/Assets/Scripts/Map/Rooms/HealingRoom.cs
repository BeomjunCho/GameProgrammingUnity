using UnityEngine;
//In this room, user can get healed 
public class HealingRoom : Room 
{
    public override string ToString()
    {
        return "Healing Room"; //representation
    }
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Healing Room.");
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You found a statue which is glowing with a sacred light.");

        }
        else
        {
            Debug.Log("You entered Healing Room.");
            Debug.Log("You have returned to the Healing Room. The statue doesn't looks sacred anymore.");
        }
    }

    public override void OnRoomSearched(Player user)
    {
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You are searching Healing Room.");
            Debug.Log("You approached a statue of a goddess. It has a sacred aura.\nYou begin to pray.");
            if (user.hp == 10)
            {
                Debug.Log("You are already fully healed. You don't get healed");
            }
            else if (user.hp >= 5 && user.hp <= 9)
            {
                int healAmount = 10 - user.hp;
                user.hp += healAmount;
                Debug.Log($"You got healed for {healAmount}.");
            }
            else
            {
                int healAmount = 5;
                user.hp += healAmount;
                Debug.Log($"You got healed for {healAmount}.");
            }
        }
        else
        {
            Debug.Log("You are searching Healing Room.");
            Debug.Log("There’s nothing to be found here except for a simple stone statue.");
        }
        user.ShowPlayerState();

    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving from Healing Room");
        IsVisited = true; // Mark the room as visited
    }

}




