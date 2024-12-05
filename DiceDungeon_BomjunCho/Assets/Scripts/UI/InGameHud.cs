using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The InGameHud class manages the in-game HUD, displaying player stats such as health, timer, and level.
/// It also handles inventory screen toggling, pause menu activation, and updates the player's health UI.
/// </summary>
public class InGameHud : MonoBehaviour
{
    [SerializeField] private UIManager _uiSystem;
    [SerializeField] private BattleHud _battleHud;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _playerHealth;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private UseItemButton _useItemButton;
    [SerializeField] private GameObject _inventoryScreen;
    [SerializeField] private TMP_Text _levelNumber;
    private Player _player;

    public bool gamePaused = true;
    private float _timerTime = 0;
    private Inventory _inventory;
    private PlayerController _playerController;

    /// <summary>
    /// Initializes the HUD at the start of the game. Timer is paused by default.
    /// </summary>
    private void Start()
    {
        // Pause timer
        _timer.text = "Timer Paused";
        _timer.color = Color.yellow;
    }

    /// <summary>
    ///  Set up In game hud for using.
    /// </summary>
    public void SetUp()
    {
        gamePaused = false;
        _player = Object.FindAnyObjectByType<Player>();
        OnHealthChange(_player.curHP, _player.maxHp);
        _battleHud.SetUp();

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

        // Find player controller in game scene
        _playerController = Object.FindAnyObjectByType<PlayerController>(); // find player controller
    }

    /// <summary>
    /// Handles initialization when the HUD is enabled, including music and cursor settings.
    /// </summary>
    private void OnEnable()
    {
        AudioManager.Instance.StopMusic(); // Stop previous music
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicList[(int)MusicTrack.InGameMusic], 0.5f); // Play In game music
        AudioManager.Instance.PlayAmbience(AudioManager.Instance.ambience.clip, 1f);
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
        Cursor.visible = false; //hide cursor

        // Prevent to open inventory screen after battle 
        if (_inventoryScreen.activeSelf)
        {
            _inventoryScreen.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the HUD every frame, handling the timer, inventory toggling, and health UI updates.
    /// </summary>
    private void Update()
    {
        if (gamePaused) //
            return;
        _timerTime += Time.deltaTime;
        _timer.text = $"{_timerTime,0:0.000}";

        if (Input.GetKeyDown(KeyCode.Tab)) //open pause menu
        {
            gamePaused = true;
            _uiSystem.OpenPauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.E)) // Toggle inventory screen in game hud
        {
            if (!_inventoryScreen.activeSelf)
            {
                // lock player camera and show cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            //refresh inventory ui
            _inventoryUI.RefreshInventoryUI();
            _inventoryUI.CloseItemDetail(); //close item detail screen
            _inventoryScreen.SetActive(!_inventoryScreen.activeSelf); // toggle active state of inventory screen

            // if inventory screen is not active, cursor lock and invisible
            if (!_inventoryScreen.activeSelf) 
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            //toggle player controller can move boolean
            if (!_inventoryScreen.activeSelf)
            {
                if (_playerController.canMove == false)
                {
                    _playerController.canMove = true;
                }
            }
            if (_inventoryScreen.activeSelf)
            {
                if (_playerController.canMove == true)
                {
                    _playerController.canMove = false;
                }
            }
        }

        OnHealthChange(_player.curHP, _player.maxHp); // update player health
        _levelNumber.text = _player.level.ToString();
    }

    /// <summary>
    /// Updates the player's health display in the HUD.
    /// </summary>
    /// <param name="currenthealth">The player's current health.</param>
    /// <param name="maxHealth">The player's maximum health.</param>
    public void OnHealthChange(float currenthealth, float maxHealth) 
    {
        _healthBar.fillAmount = currenthealth / maxHealth;
        _playerHealth.text = $"{currenthealth} / {maxHealth}";
    }


}
