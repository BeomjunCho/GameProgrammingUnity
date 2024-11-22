using UnityEngine;

//In this room, Player fights with normal monster
public class CombatRoom : Room
{
    // Array for random monster spawn
    [SerializeField] private Monster[] _normalMonsterPrefab;
    // Game object for spawning position information
    [SerializeField] private Transform _spawnPoint;

    private void Start()
    {
        // Instantiate random monster from array
        Monster NormalMonsterInstance = Instantiate(_normalMonsterPrefab[Random.Range(0, _normalMonsterPrefab.Length)], transform);
        // Move it to spawn point
        NormalMonsterInstance.transform.position = _spawnPoint.position;
        if (NormalMonsterInstance.TryGetComponent<NormalMonster>(out NormalMonster normalMonster))
        {
            normalMonster.SetUp();
        }
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
}




