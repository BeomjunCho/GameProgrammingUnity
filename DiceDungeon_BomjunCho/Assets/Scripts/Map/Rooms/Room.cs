using UnityEngine;

public abstract class Room : MonoBehaviour
{
    public bool IsVisited { get; set; } = false; // check if this room is visited

    // Room connection
    public Room North = null!;
    public Room South = null!;
    public Room East = null!;
    public Room West = null!;
    // Action in the room
    public abstract void OnRoomEntered(Player user);
    public abstract void OnRoomSearched(Player user);
    public abstract void OnRoomExit(Player user);

    // Default representation
    public override string ToString()
    {
        return "Room"; 
    }
}


