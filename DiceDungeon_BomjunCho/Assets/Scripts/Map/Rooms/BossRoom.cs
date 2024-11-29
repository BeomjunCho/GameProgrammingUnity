using UnityEngine;

/// <summary>
/// The BossRoom class represents a room where the player fights a boss monster.
/// It handles spawning the boss, managing player interactions, initiating battles, and handling player, monster position during and after the battle.
/// </summary>
public class BossRoom : Room
{
    [SerializeField] private BossMonster _bossMonsterPrefab;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private UIManager _uiSystem;

    [SerializeField] private Transform _playerBattlePosition;
    [SerializeField] private Transform _monsterBattlePosition;
    [SerializeField] private Transform _battleEndPlayerPosition;

    private Interaction _interaction;                   //Player interaction script
    private GameObject _playerInThisRoom;               //Player gameobj in this room
    private GameObject _monsterInThisRoom;              //Monster gameobj in this room
    private bool _isPlayerInThisRoom;                   //Check If player is in this toom
    private PlayerController _playerController;         //Player controller script

    /// <summary>
    /// Unity's Start method is used to initialize the BossRoom.
    /// Spawns the boss monster at the designated spawn point and sets it up.
    /// Also retrieves a reference to the UIManager.
    /// /// </summary>
    private void Start()
    {
        Monster bossMonsterInstance = Instantiate(_bossMonsterPrefab, transform);
        bossMonsterInstance.transform.position = _spawnPoint.position;
        bossMonsterInstance.transform.Rotate(0, 270f, 0);
        if (bossMonsterInstance.TryGetComponent<BossMonster>(out BossMonster bossMonster))
        {
            bossMonster.SetUp();
        }
        _uiSystem = GameObject.Find("UI_Manager").GetComponent<UIManager>();
    }

    /// <summary>
    /// Triggered when an object enters the BossRoom's collider.
    /// Activates room light, marks the player as present in the room, 
    /// retrieves player and interaction components, and identifies the monster in the room.
    /// </summary>
    /// <param name="other">The collider of the object that entered the room.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Boss Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");

            _isPlayerInThisRoom = true;
            _playerInThisRoom = other.gameObject;
            _playerController = other.GetComponent<PlayerController>();
            _interaction = other.GetComponent<Interaction>();
            _monsterInThisRoom = FindMonsterInThisRoom().gameObject;
            _interaction.currentBattleRoom = this;
        }
    }

    /// <summary>
    /// Triggered when an object exits the BossRoom's collider.
    /// Deactivates room light and marks the player as no longer present in the room.
    /// </summary>
    /// <param name="other">The collider of the object that exited the room.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Boss Room.");
            _isPlayerInThisRoom = false;
        }
    }

    /// <summary>
    /// Searches for and returns the monster present in the room.
    /// Logs a warning if no monster is found.
    /// </summary>
    /// <returns>The Monster component found in the room, or null if no monster is present.</returns>
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
    /// Temporarily disale physics of player rb and re-enable it after finishing moving to prevent crash
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

                rb.isKinematic = false; // Re-enable physics
            }
        }
    }
    /// <summary>
    /// Handles player escape behavior by stopping battle-related music or other audio.
    /// </summary>
    public void PlayerEscape()
    {
        AudioManager.Instance.StopMusic();
        
    }

}




