using UnityEngine;

//In this room, Player fights with boss monster
public class BossRoom : Room
{
    [SerializeField] private BossMonster _bossMonsterPrefab;
    [SerializeField] private Transform _spawnPoint;

    private void Start()
    {
        Monster bossMonsterInstance = Instantiate(_bossMonsterPrefab, transform);
        bossMonsterInstance.transform.position = _spawnPoint.position;
        bossMonsterInstance.transform.Rotate(0, 180f, 0);
        if (bossMonsterInstance.TryGetComponent<BossMonster>(out BossMonster bossMonster))
        {
            bossMonster.SetUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Boss Room.");
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
            Debug.Log("Player is leaving from Boss Room.");
        }
    }

}




