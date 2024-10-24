using UnityEngine;
using System.Collections.Generic;


public class Map : MonoBehaviour
{
    public GameObject startingRoomPrefab;
    public GameObject bossRoomPrefab;
    public GameObject treasureRoomPrefab;
    public GameObject combatRoomPrefab;
    public GameObject healingRoomPrefab;
    public GameObject tradeRoomPrefab;
    public GameObject libraryRoomPrefab;

    public Room currentRoom;
    public Room lastRoom; 

    List<Room> mapList = new List<Room>();

    private int mapSize =  3;
    private int allRoom;

    public void Awake() 
    {
        allRoom = (mapSize * mapSize);
        BuildRooms();
        VisualizeMap();
        currentRoom = mapList[0]; 
        lastRoom = mapList[mapList.Count - 1];
    }

        
    public void BuildRooms() // build game room. first is starting room and last is boss room always
    {
        mapList.Clear();

        GameObject startRoomObj = Instantiate(startingRoomPrefab, Vector3.zero, Quaternion.identity);
        StartingRoom startingRoom = startRoomObj.GetComponent<StartingRoom>();  // Get the StartingRoom script
        mapList.Add(startingRoom);

        for (int i = 1; i < allRoom - 1; i++)
        {
            Room rndRoom = RandomRoomGenerator();
            mapList.Add(rndRoom);
        }

        GameObject bossRoomObj = Instantiate(bossRoomPrefab, Vector3.zero, Quaternion.identity);
        BossRoom bossRoom = bossRoomObj.GetComponent<BossRoom>(); // Get the BossRoom script
        mapList.Add(bossRoom);
    }

    public Room RandomRoomGenerator()
    {
        int roomIndex = Random.Range(0, 5);

        GameObject roomObj = null; // Create a reference to hold the instantiated prefab
        Room roomComponent = null; // This will hold the Room component after instantiation

        switch (roomIndex)
        {
            case 0:
                roomObj = Instantiate(treasureRoomPrefab, Vector3.zero, Quaternion.identity);
                roomComponent = roomObj.GetComponent<TreasureRoom>(); // Get the TreasureRoom script
                break;
            case 1:
                roomObj = Instantiate(combatRoomPrefab, Vector3.zero, Quaternion.identity);
                roomComponent = roomObj.GetComponent<CombatRoom>(); // Get the CombatRoom script
                break;
            case 2:
                roomObj = Instantiate(healingRoomPrefab, Vector3.zero, Quaternion.identity);
                roomComponent = roomObj.GetComponent<HealingRoom>(); // Get the HealingRoom script
                break;
            case 3:
                roomObj = Instantiate(tradeRoomPrefab, Vector3.zero, Quaternion.identity);
                roomComponent = roomObj.GetComponent<TradeRoom>(); // Get the TradeRoom script
                break;
            case 4:
                roomObj = Instantiate(libraryRoomPrefab, Vector3.zero, Quaternion.identity);
                roomComponent = roomObj.GetComponent<LibraryRoom>(); // Get the LibraryRoom script
                break;
            default:
                roomObj = Instantiate(treasureRoomPrefab, Vector3.zero, Quaternion.identity);
                roomComponent = roomObj.GetComponent<TreasureRoom>(); // Default to TreasureRoom
                break;
        }

        return roomComponent; // Return the Room component (it will be a specific type but upcast to Room)
    }
    private void VisualizeMap()
    {
        int roomIndex = 0; // start with the first room in mapList

        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                if (roomIndex < mapList.Count) // Ensure it does not go out of bounds
                {
                    Room room = mapList[roomIndex]; // Get the room from mapList
                    GameObject roomObj = room.gameObject; // Get the GameObject of the room

                    // Move the room's GameObject to the grid position (x, z)
                    roomObj.transform.position = new Vector3(x, 0, z);


                    // Get the Renderer component of roomObj to change its color
                    Renderer roomRenderer = roomObj.GetComponent<Renderer>();


                    // Point out where is the starting room with sphere
                    if (room is StartingRoom)
                    {
                        GameObject startingRoomIndicater = GameObject.CreatePrimitive(PrimitiveType.Sphere); // create sphere primitive
                        startingRoomIndicater.transform.position = new Vector3(x, 1, z); // + 1 on Y-axis to avoid overlap
                        startingRoomIndicater.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // scale the sphere
                        startingRoomIndicater.transform.parent = roomObj.transform; // parent roomObj to sphere (Sphere is a child in roomObj)
                    }

                    roomIndex++; // Move to the next room in the list
                }
            }
        }
    }
}

