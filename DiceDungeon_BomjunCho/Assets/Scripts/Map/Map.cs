using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;


public class Map : MonoBehaviour
{
    [SerializeField] private Room[] RoomPrefabs;
    [SerializeField] private Room StartingRoomPrefab;
    [SerializeField] private Room BossRoomPrefab;
    [SerializeField] private float RoomSize = 30;
    private const int MapSize = 9;
    public Dictionary<Vector2, Room> RoomDic = new();

    /// <summary>
    ///     Create each room instances and coordinates
    ///     Store that coordinates in room instance
    ///     Add information in dictionary
    /// </summary>
    public void CreateMap(ref Inventory inventory)
    {
        //Instantiate starting room at proper coords
        Vector2 startCoords = new Vector2(0, 0);
        var startingRoomInstance = Instantiate(StartingRoomPrefab, transform);
        startingRoomInstance.SetRoomLocation(startCoords);
        startingRoomInstance.SetPlayerInventory(ref inventory);
        RoomDic.Add(startCoords, startingRoomInstance);

        //Instantiate boss room at proper coords
        Vector2 bossCoords = new Vector2((MapSize - 1) * RoomSize, (MapSize - 1) * RoomSize);
        var bossRoomInstance = Instantiate(BossRoomPrefab, transform);
        bossRoomInstance.SetRoomLocation(bossCoords);
        bossRoomInstance.SetPlayerInventory(ref inventory);
        RoomDic.Add(bossCoords, bossRoomInstance);

        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                // Skip the starting room position (0, 0) and boss room position (last cell)
                if ((x == 0 && z == 0) || (x == MapSize - 1 && z == MapSize - 1))
                    continue;

                // Coordinates for roomInstance
                Vector2 coords = new Vector2(x * RoomSize, z * RoomSize);
                
                // Create random room instance from RoomPrefabs 
                Room roomInstance = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)], transform);
                // Store that coordinates in room instance and move it to the coordinate location
                roomInstance.SetRoomLocation(coords);
                // Send player inventory to each room to use it.
                roomInstance.SetPlayerInventory(ref inventory);
                // Add coordinates information and room instance in Dictionary
                RoomDic.Add(coords, roomInstance);
            }
        }
        // Loop each room in room dictionary
        foreach (var _room in RoomDic)
        {
            Vector2 currentCoords = _room.Key;

            // Send next room coordinates and check if there is specific coordinates in dictionary
            Room northRoom = FindRoomAtPosition(currentCoords + Vector2.up * RoomSize);
            Room eastRoom = FindRoomAtPosition(currentCoords + Vector2.right * RoomSize);
            Room southRoom = FindRoomAtPosition(currentCoords + Vector2.down * RoomSize);
            Room westRoom = FindRoomAtPosition(currentCoords + Vector2.left * RoomSize);

            // Set connections in each direction
            _room.Value.SetRooms(northRoom, eastRoom, southRoom, westRoom);
        }
    }

    /// <summary>
    ///     Try to find a map with parameter coordinates in dictionary
    ///     If it finds room, return room
    ///     If it fails to find, return null
    /// </summary>
    private Room FindRoomAtPosition(Vector2 coordinates)
    {
        // Check if the dictionary contains this room coordinates
        if (RoomDic.TryGetValue(coordinates, out Room room))
        {
            // If found, return the room at those coordinates
            return room;
        }
        else
        {
            // If not found, return null (meaning no room exists at those coordinates)
            return null;
        }
    }

    public void DebugRoomPositionsAndConnections()
    {
        Debug.Log("=== Room Positions and Connections ===");

        foreach (var roomEntry in RoomDic)
        {
            Vector2 gridPosition = roomEntry.Key;
            Room room = roomEntry.Value;

            // Build the debug message
            string debugMessage = $"Room at Grid Position: {gridPosition}\n" +
                                  $"  World Position: {room.transform.position}\n" +
                                  $"  Connected Rooms:\n" +
                                  $"    North: {(room.North != null ? room.North.RoomPosition.ToString() : "None")}\n" +
                                  $"    East: {(room.East != null ? room.East.RoomPosition.ToString() : "None")}\n" +
                                  $"    South: {(room.South != null ? room.South.RoomPosition.ToString() : "None")}\n" +
                                  $"    West: {(room.West != null ? room.West.RoomPosition.ToString() : "None")}\n";

            // Log the message
            Debug.Log(debugMessage);
        }
    }
}

