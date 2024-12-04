using UnityEngine;

//Player starts game in this room.
public class StartingRoom : Room
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Starting Room.");
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
            Debug.Log("Player is leaving from Starting Room.");
        }
    }

    /*
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Starting Room.");
        Debug.Log("This is a damp, slightly dark room.\n(Enter anykey)");
    }

    public override void OnRoomSearched(Player user)
    {

        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You are searching Starting Room.");
            Debug.Log("There are dagger and potion on the floor\n(Enter anykey)");
        }
        else
        {
            Debug.Log("You are searching Starting Room.");
            Debug.Log("Nothing is here.");
        }
        //user.ShowPlayerState();
        Debug.Log("Do you want to leave this room?\n(Enter anykey)");

    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving from Starting Room");
        IsVisited = true; // Mark the room as visited
    }
    */
}




