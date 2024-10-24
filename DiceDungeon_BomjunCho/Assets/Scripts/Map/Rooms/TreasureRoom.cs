using UnityEngine;

//In this room, user gets random items
public class TreasureRoom : Room
{
    public override string ToString()
    {
        return "Treasure Room"; //representation
    }
    public override void OnRoomEntered(Player user)
    {
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You entered Treasure Room");
            Debug.Log("There is one treasure chest on center of this room.\n(Enter anykey)");

        }
        else
        {
            Debug.Log("You entered Treasure Room");
            Debug.Log("You have returned to the Treasure Room. The room looks the same as before except for opened chest.");
        }
    }

    public override void OnRoomSearched(Player user)
    {
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You are searching Treasure Room");
            Debug.Log("You opened the chest and there is one item!");
            user.inventory.TreasureItemAdd();

        }
        else
        {
            Debug.Log("You are searching Treasure Room");
            Debug.Log("There is nothing left to search in this room.");
        }
        user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving from Treasure Room");
        IsVisited = true; // Mark the room as visited
    }
}




