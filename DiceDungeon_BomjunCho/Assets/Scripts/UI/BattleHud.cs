using UnityEngine.UI;
using UnityEngine;
using TMPro;

/// <summary>
/// The BattleHud class manages the battle interface, including health displays, inventory, and player interactions.
/// It connects various systems like the player's inventory, UI, and battle mechanics.
/// </summary>
public class BattleHud : MonoBehaviour
{
    private Player _player;                      // Reference to the Player instance.
    private Interaction _interaction;           // Reference to the Player's interaction component.
    private PlayerController _playerController; // Reference to the Player's movement controller.

    [SerializeField] private UIManager _uiSystem;             // Reference to the UIManager for managing HUD transitions.
    [SerializeField] private InventoryUI _inventoryUI;        // Reference to the inventory UI component.
    [SerializeField] private UseItemButton _useItemButton;    // Button for using items in battle.
    [SerializeField] private GameObject _itemDiscription;     // UI element displaying item descriptions.
    [SerializeField] private BattleManager _battleManager;    // Reference to the BattleManager for battle logic.

    [SerializeField] private Image _currentHealth_P;          // Player's current health display (UI).
    [SerializeField] private Image _currentHealth_M;          // Monster's current health display (UI).
    [SerializeField] private TMP_Text _playerMaxHealth;       // Player's health text display.
    [SerializeField] private TMP_Text _monsterMaxHealth;      // Monster's health text display.

    public Monster _monster;                                 // Reference to the current monster in the battle.
    private Inventory _inventory;                            // Reference to the player's inventory.
    
    
    /// <summary>
    /// Sets up the BattleHud, connecting necessary components like the player's inventory and battle manager.
    /// </summary>
    public void SetUp()
    {
        // FInd Inventory in game scene.
        _inventory = Object.FindAnyObjectByType<Inventory>();
        if (_inventory != null)
        {
            _inventoryUI.SetUp(_inventory);
            _useItemButton.SetUp(_inventory);
        }
        else
        {
            Debug.LogWarning("Inventory is not set correctly.");
        }
        _player = Object.FindAnyObjectByType<Player>();
        _interaction = _player.gameObject.GetComponent<Interaction>();
        _playerController = _player.gameObject.GetComponent<PlayerController>();
        _battleManager.SetUp(_player);
    }

    /// <summary>
    /// Handles the "Run Away" button logic, exiting the battle and returning the player to their previous position.
    /// </summary>
    public void ButtonRunAway()
    {
        _uiSystem.OpenInGameHud();

        Debug.Log("Battle ended.");
        _interaction.isPlayerFighting = false;

        Room currentBattleRoom = _interaction.currentBattleRoom;

        if (currentBattleRoom is CombatRoom combatRoom)
        {
            combatRoom.MovePlayerToEndBattlePos();
        }
        else if (currentBattleRoom is BossRoom bossRoom)
        {
            bossRoom.MovePlayerToEndBattlePos();
        }
    }

    /// <summary>
    /// Updates the health display for either the player or the monster.
    /// </summary>
    /// <param name="currentHealth">The current health value.</param>
    /// <param name="maxHealth">The maximum health value.</param>
    /// <param name="shield">The player's shield value (ignored for monsters).</param>
    /// <param name="isPlayer">Indicates whether to update the player's or monster's health UI.</param>
    public void OnHealthChange(float currentHealth, float maxHealth, float shield, bool isPlayer = true)
    {
        if (isPlayer)
        {
            _currentHealth_P.fillAmount = currentHealth / maxHealth;
            _playerMaxHealth.text = $"{currentHealth}/{maxHealth} / shield: {shield}";
        }
        else
        {
            _currentHealth_M.fillAmount = currentHealth / maxHealth;
            _monsterMaxHealth.text = $"{currentHealth}/{maxHealth}";
        }
    }

    /// <summary>
    /// Prepares the BattleHud for display, initializing UI elements and starting the battle.
    /// </summary>
    private void OnEnable()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicList[(int)MusicTrack.BattleMusic], 0.3f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _playerController.canMove = false;

        _monster = _interaction.ReturnDetectedMonster();

        _useItemButton.GetMonster(_monster);
        _battleManager.GetMonster(_monster);

        _inventoryUI.RefreshInventoryUI();

        _battleManager.StartBattle();
    }

    /// <summary>
    /// Cleans up the BattleHud when it is disabled, re-enabling player movement and hiding UI elements.
    /// </summary>
    private void OnDisable()
    {
        _playerController.canMove = true;
        _itemDiscription.SetActive(false);
    }

}
