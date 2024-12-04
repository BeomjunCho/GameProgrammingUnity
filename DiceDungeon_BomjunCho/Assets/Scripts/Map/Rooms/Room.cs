using UnityEngine;

/// <summary>
/// Abstract base class for rooms in a game. 
/// Handles room positioning, connections to neighboring rooms, and inventory interactions.
/// </summary>
public abstract class Room : MonoBehaviour
{
    // Doorways for each direction (north, east, south, west)
    [SerializeField] public GameObject NorthDoorWay, EastDoorWay, SouthDoorWay, WestDoorWay;

    // Stone doors for blocking pathways
    [SerializeField] private GameObject StoneDoorNorth, StoneDoorSouth, StoneDoorWest, StoneDoorEast;

    // Room light object
    [SerializeField] public GameObject RoomLight;

    // References to adjacent rooms
    private Room _north, _south, _east, _west;

    // Public accessors for neighboring rooms
    public Room North => _north;
    public Room South => _south;
    public Room West => _west;
    public Room East => _east;

    // Room's 2D grid position in the map
    private Vector2 _roomPosition;
    public Vector2 RoomPosition => _roomPosition;

    // Reference to the player's inventory
    protected Inventory _playerInventory;

    /// <summary>
    /// Sets the player's inventory reference, allowing room interactions with the inventory.
    /// Called from the GameController with an inventory instance.
    /// </summary>
    /// <param name="inventory">Reference to the player's inventory.</param>
    public void SetPlayerInventory(ref Inventory inventory)
    {
        _playerInventory = inventory;
    }

    /// <summary>
    /// Sets the room's position in the game world based on 2D grid coordinates.
    /// </summary>
    /// <param name="coordinates">The room's new position as a 2D vector.</param>
    public virtual void SetRoomLocation(Vector2 coordinates)
    {
        // Move this room to the specified 2D grid position
        transform.position = new Vector3(coordinates.x, 0, coordinates.y);
        // Store the coordinates as the room's position
        _roomPosition = coordinates;
    }

    /// <summary>
    /// Sets the neighboring rooms and adjusts doorways and stone doors based on connections.
    /// </summary>
    /// <param name="NorthRoom">The room to the north, or null if there isn't one.</param>
    /// <param name="EastRoom">The room to the east, or null if there isn't one.</param>
    /// <param name="SouthRoom">The room to the south, or null if there isn't one.</param>
    /// <param name="WestRoom">The room to the west, or null if there isn't one.</param>
    public void SetRooms(Room NorthRoom, Room EastRoom, Room SouthRoom, Room WestRoom)
    {
        // Assign the neighboring rooms
        _north = NorthRoom;
        _east = EastRoom;
        _south = SouthRoom;
        _west = WestRoom;

        // Manage doorway and stone door visibility for each direction

        // North direction
        NorthDoorWay.SetActive(_north == null); // Show doorway if there's no room to the north
        StoneDoorNorth.SetActive(_north != null); // Show stone door if there's a room to the north

        // East direction
        EastDoorWay.SetActive(_east == null); // Show doorway if there's no room to the east
        StoneDoorEast.SetActive(_east != null); // Show stone door if there's a room to the east

        // South direction
        SouthDoorWay.SetActive(_south == null); // Show doorway if there's no room to the south
        StoneDoorSouth.SetActive(_south != null); // Show stone door if there's a room to the south

        // West direction
        WestDoorWay.SetActive(_west == null); // Show doorway if there's no room to the west
        StoneDoorWest.SetActive(_west != null); // Show stone door if there's a room to the west
    }
}
