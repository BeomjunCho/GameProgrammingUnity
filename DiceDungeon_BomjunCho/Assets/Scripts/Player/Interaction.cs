using TMPro;
using UnityEngine;

/// <summary>
/// The Interaction class manages player interactions with various objects in the game world.
/// It handles detecting objects within a specific range, displaying interaction prompts, 
/// and performing actions such as picking up items, opening chests, or initiating battles.
/// </summary>
public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera; // Reference to the player's camera for raycasting
    [SerializeField] private float _interactionRange; // Maximum range for interaction

    private bool _isLookingAtSomething = false; // Tracks if the player is looking at an interactable object
    private TextMeshProUGUI _interactionIndicator; // UI element for interaction hints
    private Inventory _inventory; // Reference to the player's inventory

    private UIManager _uiManager; // Reference to the UI manager
    public Monster detectedmonster; // Stores the currently detected monster
    public bool isPlayerFighting = false; // Tracks if the player is currently in a fight
    public Room currentBattleRoom; // Reference to the current battle room

    private RaycastHit _raycastHit; // Cached result of the raycast

    /// <summary>
    /// Sets up references for interaction-related components.
    /// </summary>
    /// <param name="inventory">Reference to the player's inventory.</param>
    public void SetUp(ref Inventory inventory)
    {
        // Find and assign the interaction indicator UI and UI manager
        GameObject indicatorObject = GameObject.Find("UI_Manager/Indicator");
        GameObject uiManagerObj = GameObject.Find("UI_Manager");

        _uiManager = uiManagerObj.GetComponent<UIManager>();

        if (indicatorObject != null)
        {
            _interactionIndicator = indicatorObject.GetComponent<TextMeshProUGUI>();
            _interactionIndicator.gameObject.SetActive(false); // Hide the indicator by default
        }
        else
        {
            Debug.LogWarning("Interaction indicator UI not found.");
        }

        _inventory = inventory;
    }

    private void Update()
    {
        // Only allow interactions if the player is not in a fight
        if (!isPlayerFighting)
        {
            CheckObjectOnRay(); // Perform a raycast to detect interactable objects
        }

        // Handle interactions when the player presses the "F" key
        if (Input.GetKeyDown(KeyCode.F) && _isLookingAtSomething)
        {
            HandleInteraction(); // Perform the appropriate interaction
        }

    }

    /// <summary>
    /// Performs a single raycast to check for interactable objects.
    /// </summary>
    private void CheckObjectOnRay()
    {
        // Reset interaction state and hide the indicator
        _isLookingAtSomething = false;
        _interactionIndicator.gameObject.SetActive(false);

        // Create a ray starting from the player's camera
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);

        // Perform the raycast and check for hits
        if (Physics.Raycast(ray, out _raycastHit, _interactionRange))
        {
            // Check for specific interactable components on the hit object
            if (_raycastHit.collider.TryGetComponent<Item>(out Item item))
            {
                _interactionIndicator.text = "Get Item (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<Chest>(out Chest chest))
            {
                _interactionIndicator.text = "Open Chest (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<ScrollBook>(out ScrollBook scrollBook))
            {
                _interactionIndicator.text = "Summon Scroll Item (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<AngelStatue>(out AngelStatue angelStatue))
            {
                _interactionIndicator.text = "Pray for Angel (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<TradeDemon>(out TradeDemon tradeDemon))
            {
                _interactionIndicator.text = "Trade (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<Monster>(out Monster monster))
            {
                _interactionIndicator.text = "Fight (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
            else if (_raycastHit.collider.TryGetComponent<BreakableWall>(out BreakableWall breakableWall))
            {
                _interactionIndicator.text = "Break the wall (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtSomething = true;
            }
        }
    }

    /// <summary>
    /// Handles interactions based on the detected object.
    /// </summary>
    private void HandleInteraction()
    {
        // Use the cached RaycastHit to determine the type of interaction
        if (_raycastHit.collider.TryGetComponent<Item>(out Item item))
        {
            PickUpItem(item);
        }
        else if (_raycastHit.collider.TryGetComponent<Chest>(out Chest chest))
        {
            OpenChest(chest);
        }
        else if (_raycastHit.collider.TryGetComponent<ScrollBook>(out ScrollBook scrollBook))
        {
            GetScroll(scrollBook);
        }
        else if (_raycastHit.collider.TryGetComponent<AngelStatue>(out AngelStatue angelStatue))
        {
            HealPlayer(angelStatue);
        }
        else if (_raycastHit.collider.TryGetComponent<TradeDemon>(out TradeDemon tradeDemon))
        {
            Trade(tradeDemon);
        }
        else if (_raycastHit.collider.TryGetComponent<Monster>(out Monster monster))
        {
            BattleStart(monster);
        }
        else if (_raycastHit.collider.TryGetComponent<BreakableWall>(out BreakableWall breakableWall))
        {
            BreakWall();
        }
    }

    /// <summary>
    /// Picks up an item and adds it to the player's inventory.
    /// </summary>
    private void PickUpItem(Item item)
    {
        _inventory.AddItem(item.ID); // Add the item to the inventory
        Destroy(item.gameObject); // Remove the item from the scene
        Debug.Log($"Picked up item: {item.name}");
    }

    /// <summary>
    /// Opens a chest.
    /// </summary>
    private void OpenChest(Chest chest)
    {
        chest.OpenChest(); // Open the chest
        Debug.Log($"Opened chest: {chest.name}");
    }

    /// <summary>
    /// Uses a scroll book to summon an item.
    /// </summary>
    private void GetScroll(ScrollBook scrollBook)
    {
        scrollBook.ScrollSummon(); // Summon an item from the scroll
        Debug.Log($"Used scroll: {scrollBook.name}");
    }

    /// <summary>
    /// Heals the player using an angel statue.
    /// </summary>
    private void HealPlayer(AngelStatue angelStatue)
    {
        angelStatue.HealPlayer(); // Heal the player
        Debug.Log("Healed by Angel Statue.");
    }

    /// <summary>
    /// Initiates a trade with a demon.
    /// </summary>
    private void Trade(TradeDemon tradeDemon)
    {
        tradeDemon.TradeWithDemon(); // Perform a trade
        Debug.Log("Traded with Demon.");
    }

    /// <summary>
    /// Starts a battle with a detected monster.
    /// </summary>
    private void BattleStart(Monster monster)
    {
        isPlayerFighting = true; // Set the player to fighting mode
        detectedmonster = monster; // Set the detected monster
        Debug.Log($"Started battle with: {monster.name}");
        _interactionIndicator.gameObject.SetActive(false);
        _uiManager.OpenBattleMenu(); // Open the battle menu UI
    }

    /// <summary>
    /// Open Escape screen and start game end sequence
    /// </summary>
    private void BreakWall()
    {
        if (_inventory.DoesPlayerHave(7))
        {
            _uiManager.OpenEscapeScreen();
        }

    }

    /// <summary>
    /// Returns the currently detected monster.
    /// </summary>
    /// <returns>The detected monster.</returns>
    public Monster ReturnDetectedMonster()
    {
        return detectedmonster;
    }
}
