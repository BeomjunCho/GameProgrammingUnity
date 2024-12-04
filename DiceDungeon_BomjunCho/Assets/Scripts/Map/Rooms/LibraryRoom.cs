using UnityEngine;

//In this room, Player can get scrolls depends on their luck
public class LibraryRoom : Room
{
    private void OnTriggerEnter(Collider other) // Trigger execute with player tag
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the Library Room.");
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
            Debug.Log("Player is leaving from Library Room.");
        }
    }
    
    /*
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Library Room.");
        Debug.Log("The room is filled with ancient books and scrolls. There's a mysterious aura in the air.\n(Enter anykey)");
    }

    public override void OnRoomSearched(Player user)
    {
        Debug.Log("You are searching the Library Room.");
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You scan the shelves and find an old tome with strange symbols.");

            int index = Random.Range(1, 4);
            switch (index) // randomly get fire scroll, shield scroll, nothing
            {
                case 1:
                    Debug.Log("You discover a scroll with a fire spell you can use in battle.");

                    break;
                case 2:
                    Debug.Log("You discover a scroll with a shield spell you can use in battle.");

                    break;
                default:
                    Debug.Log("You found some interesting lore, but it doesn’t seem immediately useful.");
                    break;
            }

        }
        else
        {
            Debug.Log("There doesn't seem to be anything new to find here.");
        }

        //user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving the Library Room.");
        IsVisited = true; // Mark the room as visited
    }
    */
}




