using UnityEngine;

//In this room, user can get scrolls depends on their luck
public class LibraryRoom : Room
{
    public override string ToString()
    {
        return "Library Room"; // representation
    }

    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Library Room.");
        Debug.Log("The room is filled with ancient books and scrolls. There's a mysterious aura in the air.\n(Enter anykey)");
    }

    public override void OnRoomSearched(Player user)
    {
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You are searching the Library Room.");
            Debug.Log("You scan the shelves and find an old tome with strange symbols.");

            int index = Random.Range(1, 4);
            switch (index) // randomly get fire scroll, shield scroll, nothing
            {
                case 1:
                    Debug.Log("You discover a scroll with a fire spell you can use in battle.");
                    FireScroll fireScroll = new FireScroll();
                    user.inventory.AddItem(fireScroll);
                    break;
                case 2:
                    Debug.Log("You discover a scroll with a shield spell you can use in battle.");
                    ShieldScroll shieldScroll = new ShieldScroll();
                    user.inventory.AddItem(shieldScroll);
                    break;
                default:
                    Debug.Log("You found some interesting lore, but it doesn’t seem immediately useful.");
                    break;
            }

        }
        else
        {
            Debug.Log("You are searching the Library Room.");
            Debug.Log("There doesn't seem to be anything new to find here.");
        }

        user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving the Library Room.");
        IsVisited = true; // Mark the room as visited
    }
}




