using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera; // Player camera on head
    [SerializeField] private float _interactionRange; // Interaction range to interact with game object
    [SerializeField] private BattleHud _battleHud;
    private bool _isLookingAtItem = false; // check if player looks at item
    private bool _isLookingAtChest = false; // check if player looks at chest
    private bool _isLookingAtScroll = false; // check if player looks at scroll book
    private bool _isLookingAtStatue = false; // check if player looks at angel statue
    private bool _isLookingAtDemon = false; // check if player looks at trade demon
    private bool _isLookingAtMonster = false; // check if player looks at monster
    private TextMeshProUGUI _interactionIndicator; // text UI for showing which object is player looking
    Inventory _inventory; // To save inventory from game controller
    private UIManager _uiManager;
    
    public Monster detectedmonster;

    public void SetUp(ref Inventory inventory)
    {
        // Find and assign the TextMeshProUGUI component at runtime
        GameObject IndicatorObject = GameObject.Find("UI_Manager/Indicator"); // Find indicator in game scene
        GameObject uiManagerObj = GameObject.Find("UI_Manager");

        _uiManager = uiManagerObj.GetComponent<UIManager>();

        if (IndicatorObject != null)
        {
            _interactionIndicator = IndicatorObject.GetComponent<TextMeshProUGUI>(); // Assign that indicator game object
        }

        if (_interactionIndicator != null)
        {
            _interactionIndicator.gameObject.SetActive(false); // Make sure it is initially hidden
        }
        else
        {
            Debug.LogWarning("Item indicator UI not found in the scene.");
        }

        _inventory = inventory; // Assign inventory from game controller


    }

    private void Update()
    {
        //Debug.Log($"UPDATE {detectedmonster}");
        // Check object per frames
        CheckObjectOnRay();

        if (Input.GetKeyDown(KeyCode.E) && _isLookingAtItem) // "E" for interaction with item
        {
            TryPickUpItem();
        }

        if (Input.GetKeyDown(KeyCode.F) && _isLookingAtChest) // "F" for interaction with chest
        {
            TryOpenChest();
        }
        if (Input.GetKeyDown(KeyCode.F) && _isLookingAtScroll) // "F" for interaction with scroll book
        {
            TryGetScroll();
        }
        if (Input.GetKeyDown(KeyCode.F) && _isLookingAtStatue) // "F" for interaction with angel statue
        {
            TryHealPlayer();
        }
        if (Input.GetKeyDown(KeyCode.F) && _isLookingAtDemon) // "F" for interaction with angel statue
        {
            TryTrade();
        }
        if (Input.GetKeyDown(KeyCode.F) && _isLookingAtMonster) // "F" for interaction with angel statue
        {
            Debug.Log("Start fight");
            BattleStart();
        }
    }

    void CheckObjectOnRay()
    {
        if (_interactionIndicator == null) return; // Exit if interactionIndicator is not assigned

        // Ray starts from camera position and it goes foward from it
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        // if ray hits in interaction range
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange))
        {
            if (hit.collider.TryGetComponent<Item>(out Item item)) // if object is item
            {
                // Indicate that the player is looking at an item
                _interactionIndicator.text = "Get Item(E)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtItem = true;
                return;
            }

            if (hit.collider.TryGetComponent<Chest>(out Chest chest)) // if object is chest
            {
                // Indicate that the player is looking at a chest
                _interactionIndicator.text = "Open Chest (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtChest = true;
                return;
            }

            if (hit.collider.TryGetComponent<ScrollBook>(out ScrollBook scrollBook))
            {
                // Indicate that the player is looking at a scroll book
                _interactionIndicator.text = "Summon scroll item (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtScroll = true;
                return;
            }

            if (hit.collider.TryGetComponent<AngelStatue>(out AngelStatue angelStatue))
            {
                // Indicate that the player is looking at an angel statue.
                _interactionIndicator.text = "Pray for angel (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtStatue = true;
                return;
            }

            if (hit.collider.TryGetComponent<TradeDemon>(out TradeDemon tradeDemon))
            {
                // Indicate that the player is looking at an angel statue.
                _interactionIndicator.text = "Trade (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtDemon = true;
                return;
            }

            if (hit.collider.TryGetComponent<Monster>(out Monster monster))
            {
                // Indicate that the player is looking at a monster
                _interactionIndicator.text = "Fight (F)";
                _interactionIndicator.gameObject.SetActive(true);
                _isLookingAtMonster = true;
                return;
            }
        }

        // If no item is detected, hide the indicator
        _interactionIndicator.gameObject.SetActive(false);
        _isLookingAtItem = false;
        _isLookingAtChest = false;
        _isLookingAtScroll = false;
        _isLookingAtStatue = false;
        _isLookingAtDemon = false;
        _isLookingAtMonster = false;
    }

    void TryPickUpItem()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange))
        {
            // Get the GameObject that was hit by the raycast
            GameObject hitObject = hit.collider.gameObject;

            // Check if the GameObject has an Item script component attached
            if (hitObject.TryGetComponent<Item>(out Item item))
            {
                Debug.Log("Item detected");

                if (item != null)
                {
                    _inventory.AddItem(item.ID); // Add the item to the player's inventory

                    // Destroy the item from the game scene after adding it to the inventory
                    Destroy(hitObject);
                }
                else
                {
                    Debug.LogWarning("No corresponding item prefab found in the inventory.");
                }
            }
            else
            {
                Debug.Log("The object is not an Item.");
            }
        }
    }
    void TryOpenChest()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange))
        {
            // Check if the GameObject hit by the ray has a Chest script component attached
            if (hit.collider.TryGetComponent<Chest>(out Chest chest))
            {
                chest.OpenChest(); // Call the OpenChest method on the chest
            }
            else
            {
                Debug.Log("The object is not a Chest.");
            }
        }
    }

    void TryGetScroll()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange))
        {
            if (hit.collider.TryGetComponent<ScrollBook>(out ScrollBook scrollbook))
            {
                scrollbook.ScrollSummon();
            }
            else
            {
                Debug.Log("The object is not a ScrollBook.");
            }
        }
    }

    void TryHealPlayer()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange))
        {
            if (hit.collider.TryGetComponent<AngelStatue>(out AngelStatue angelStatue))
            {
                angelStatue.HealPlayer();
            }
            else
            {
                Debug.Log("The object is not a Angel Statue.");
            }
        }
    }

    void TryTrade()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange))
        {
            if (hit.collider.TryGetComponent<TradeDemon>(out TradeDemon tradeDemon))
            {
                tradeDemon.TradeWithDemon();
            }
            else
            {
                Debug.Log("The object is not a Trade Demon.");
            }
        }
    }

    void BattleStart()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionRange))
        {
            Debug.Log("Battle start");
            // Check if the hit object is a NormalMonster
            if (hit.collider.TryGetComponent<NormalMonster>(out NormalMonster normalMonster))
            {
                detectedmonster = normalMonster;
                Debug.Log("Normal Monster detected.");
                Debug.Log($"{detectedmonster}");
                _uiManager.OpenBattleMenu();
            }
            // Check if the hit object is a BossMonster
            else if (hit.collider.TryGetComponent<BossMonster>(out BossMonster bossMonster))
            {
                detectedmonster = bossMonster;
                Debug.Log("Boss Monster detected.");
                Debug.Log($"{detectedmonster}");
                _uiManager.OpenBattleMenu();
            }
            else
            {
                Debug.Log("The object is not a monster.");
            }
           
        }
    }
    public void LogDetectedMonster()
    {
        Debug.Log("Current detected monster: " + detectedmonster);
    }

    public Monster ReturnDetectedMonster()
    {
        return detectedmonster;
    }

}
