using UnityEngine;

//In this room, Player gets random items
public class TreasureRoom : Room
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Treasure Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Treasure Room.");
        }
    }
    /*
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Treasure Room");

        if (!IsVisited) // is this room visited?
        {
            Debug.Log("There is one treasure chest on center of this room.\n(Enter anykey)");

        }
        else
        {
            Debug.Log("You have returned to the Treasure Room. The room looks the same as before except for opened chest.");
        }
    }

    public override void OnRoomSearched(Player user)
    {
        Debug.Log("You are searching Treasure Room");

        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You opened the chest and there is one item!");

        }
        else
        {
            Debug.Log("There is nothing left to search in this room.");
        }
        //user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving from Treasure Room");
        IsVisited = true; // Mark the room as visited
    }
    */
}




