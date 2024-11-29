using UnityEngine;

/// <summary>
/// Represents a Combat Room where the Player fights with a randomly spawned normal monster.
/// </summary>
public class CombatRoom : Room
{
    // Array for random monster spawn
    [SerializeField] private Monster[] _normalMonsterPrefab;

    // Game object for spawning position information
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _playerBattlePosition;
    [SerializeField] private Transform _monsterBattlePosition;
    [SerializeField] private Transform _battleEndPlayerPosition;

    private Interaction _interaction;                   //Player interaction script
    private GameObject _playerInThisRoom;               //Player gameobj in this room
    private GameObject _monsterInThisRoom;              //Monster gameobj in this room
    private bool _isPlayerInThisRoom;                   //Check If player is in this toom
    private PlayerController _playerController;         //Player controller script

    /// <summary>
    /// Spawns and sets up a monster in the room.
    /// Also retrieves the Interaction component for player interactions.
    /// </summary>
    private void Start()
    {
        // Instantiate random monster and monster set up
        SpawnAndInitializeMonster();
        _interaction = Object.FindAnyObjectByType<Interaction>();
    }

    /// <summary>
    /// If the collider belongs to the Player, it prepares battle sequence.
    /// Find Player game obj and player controller in this room
    /// Find monster in this room
    /// update current combat room in interaction script
    /// </summary>
    /// <param name="other">The Collider entering the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Combat Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");

            _isPlayerInThisRoom = true;
            _playerInThisRoom = other.gameObject;
            _playerController = other.GetComponent<PlayerController>();
            _monsterInThisRoom = FindMonsterInThisRoom().gameObject;
            _interaction.currentBattleRoom = this;
        }
    }

    /// <summary>
    /// If the collider belongs to the Player, it turns off room lighting and logs the player's exit.
    /// </summary>
    /// <param name="other">The Collider exiting the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Combat Room.");
            _isPlayerInThisRoom = false;
        }
    }

    /// <summary>
    /// Spawns a random monster from the `_normalMonsterPrefab` array at the designated spawn point.
    /// If the spawned monster is of type `NormalMonster`, it is set up accordingly.
    /// </summary>
    private void SpawnAndInitializeMonster()
    {
        Monster monsterInstance = Instantiate(
            _normalMonsterPrefab[Random.Range(0, _normalMonsterPrefab.Length)],
            transform
        );
        monsterInstance.transform.position = _spawnPoint.position;

        if (monsterInstance.TryGetComponent<NormalMonster>(out NormalMonster normalMonster))
        {
            normalMonster.SetUp();
        }
    }

    /// <summary>
    /// Searches the room for a Monster component in its child objects.
    /// </summary>
    /// <returns>The Monster component if found; otherwise, null.</returns>
    private Monster FindMonsterInThisRoom()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Monster>(out Monster monster))
            {
                return monster;
            }
        }

        Debug.LogWarning("Monster is not found in this room.");
        return null;
    }

    /// <summary>
    /// Moves the player and monster to their battle positions if a fight is ongoing.
    /// Updates the player's camera to focus on the monster.
    /// </summary>
    public void ReadyToBattle()
    {
        if (_isPlayerInThisRoom)
        {
            MoveToBattlePos(_playerInThisRoom.transform, _playerBattlePosition.position);
            MoveToBattlePos(_monsterInThisRoom.transform, _monsterBattlePosition.position);
            _playerInThisRoom.transform.LookAt(_monsterBattlePosition.position);
            _playerController.CameraLookAtObject(_monsterBattlePosition);
        }
    }

    /// <summary>
    /// Moves an object to a specified position if it is not already there.
    /// </summary>
    /// <param name="obj">The Transform of the object to move.</param>
    /// <param name="targetPosition">The target position to move the object to.</param>
    private void MoveToBattlePos(Transform obj, Vector3 targetPosition)
    {
        if (Vector3.Distance(obj.position, targetPosition) > 0.01f)
        {
            obj.position = targetPosition;
        }
    }

    /// <summary>
    /// Move player to battle end position
    /// Temporarily disale physics of player rb and re-enable it after finishing moving to prevent physical crash
    /// </summary>
    public void MovePlayerToEndBattlePos()
    {
        if (_isPlayerInThisRoom)
        {
            Debug.Log("Battle is ended, moving player to EndBattlePosition");
            Rigidbody rb = _playerInThisRoom.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true; // Temporarily disable physics

                //Move player to battle end position
                _playerInThisRoom.transform.position = _battleEndPlayerPosition.position;
                //Set player rotation to normal
                _playerInThisRoom.transform.rotation = Quaternion.identity;

                rb.isKinematic = false; // Re-enable physics
            }
        }
    }
}
