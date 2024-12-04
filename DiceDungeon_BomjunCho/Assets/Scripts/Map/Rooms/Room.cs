using System;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    // Room connection way
    [SerializeField] public GameObject NorthDoorWay, EastDoorWay, SouthDoorWay, WestDoorWay;
    // Stone door for each way
    [SerializeField] GameObject StoneDoorNorth, StoneDoorSouth, StoneDoorWest, StoneDoorEast;
    // Light for room
    [SerializeField] public GameObject RoomLight;

    // Room direction
    private Room _north, _south, _east, _west;
    public Room North => _north;
    public Room South => _south;
    public Room West => _west;
    public Room East => _east;

    private Vector2 _roomPosition;
    public Vector2 RoomPosition => _roomPosition;

    public Inventory _playerInventory;

    // Set Player Inventory in GameController(Passing Inventory instance from GameController)
    public void SetPlayerInventory(ref Inventory inventory)
    {
        _playerInventory = inventory;
    }

    //Set Room
    public virtual void SetRoomLocation(Vector2 coordinates)
    {
        // move this room to coordinates
        transform.position = new Vector3(coordinates.x, 0, coordinates.y);
        // Store that coordinates information
        _roomPosition = coordinates;
    }

    public void SetRooms(Room NorthRoom, Room EastRoom, Room SouthRoom, Room WestRoom) //Active Doorway if there is no room in the direction
    {
        _north = NorthRoom;
        NorthDoorWay.SetActive(_north == null); // Set active door way if there is room on that direction
        StoneDoorNorth.SetActive(_north != null); // Set inactive StoneDoor if there is no room on that direction
        _east = EastRoom;
        EastDoorWay.SetActive(_east == null); // Set active door way if there is room on that direction
        StoneDoorEast.SetActive(_east != null);// Set inactive StoneDoor if there is no room on that direction
        _south = SouthRoom;
        SouthDoorWay.SetActive(_south == null); // Set active door way if there is room on that direction
        StoneDoorSouth.SetActive(_south != null);// Set inactive StoneDoor if there is no room on that direction
        _west = WestRoom;
        WestDoorWay.SetActive(_west == null); // Set active door way if there is room on that direction
        StoneDoorWest.SetActive(_west != null);// Set inactive StoneDoor if there is no room on that direction
    }
}


