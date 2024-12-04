using UnityEngine;

//In this room, Player fights with normal monster
public class CombatRoom : Room
{
    // Array for random monster spawn
    [SerializeField] private Monster[] NormalMonsterPrefab;
    // Game object for spawning position information
    [SerializeField] private Transform MonsterSpawnPoint;

    private void Start()
    {
        // Instantiate random monster from array
        Monster NormalMonsterInstance = Instantiate(NormalMonsterPrefab[Random.Range(0, NormalMonsterPrefab.Length)], transform);
        // Move it to spawn point
        NormalMonsterInstance.transform.position = MonsterSpawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Combat Room.");
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
            Debug.Log("Player is leaving from Combat Room.");
        }
    }



    /*
    public override void OnRoomEntered(Player user)
    {
        Debug.Log("You entered Combat Room.");
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You’ve encountered a fierce Minotaur. Prepare for battle!\n(Enter anykey)");
            NormalMonster monster = new NormalMonster();
            DieRoller dieRoller = new DieRoller();
            dieRoller.GamePlayLoop(user, monster); // dice roll battle starts
        }
        else
        {
            Debug.Log("You have returned to the Combat Room. There is a dead monster.");
        }
    }

    public override void OnRoomSearched(Player user)
    {
        Debug.Log("You are searching Combat Room.");
        if (!IsVisited) // is this room visited?
        {
            Debug.Log("You found one item from dead monster.");
        }
        else
        {
            Debug.Log("There is nothing left to search in this room.");
        }
        //user.ShowPlayerState();
    }

    public override void OnRoomExit(Player user)
    {
        Debug.Log("You are leaving from Combat Room");
        IsVisited = true; // Mark the room as visited
    }
    */
}




