using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The UseItemButton class handles the logic for using items from the inventory,
/// including player and monster interactions during battles and regular gameplay.
/// </summary>
public class UseItemButton : MonoBehaviour
{
    [SerializeField] private InventoryUI _inventoryUi;        // Reference to the Inventory UI for refreshing after item usage.
    [SerializeField] private GameObject _itemDetailPanel;     // Panel showing details of the selected item.
    [SerializeField] private GameObject _InGameHud;           // Reference to the in-game HUD for context checks.
    [SerializeField] private BattleManager _battleManager;    // Reference to the BattleManager for handling battle actions.

    private Inventory _inventory;                             // Reference to the player's inventory.
    private Item _item;                                       // The currently selected item.
    private Monster _monster;                                 // The monster currently being interacted with during battles.
    public Button button;                                     // The button component attached to this script.
    public bool ItemActionCompleted = false;                 // Tracks whether the item action has been completed.

    /// <summary>
    /// Sets up the UseItemButton with the player's inventory.
    /// </summary>
    /// <param name="inventory">The player's inventory instance.</param>
    public void SetUp(Inventory inventory)
    {
        _inventory = inventory;
        button = GetComponent<Button>();
    }

    /// <summary>
    /// Initializes the button's state for interaction.
    /// </summary>
    public void SetUp()
    {
        ItemActionCompleted = false;
        button.interactable = true;
    }

    /// <summary>
    /// Executes the appropriate action based on the type of the selected item.
    /// Updates the inventory and item details UI, and sends results to the BattleManager if in battle.
    /// </summary>
    public void UseItem()
    {
        string itemAction = "";

        // Handle different item types with specific logic.
        if (_item is HealingPotion healingPotion)
        {
            healingPotion.GetPlayer();
            healingPotion.Heal();
            _inventory.RemoveItem(_item.ID);
            _inventoryUi.RefreshInventoryUI();
            itemAction = _item.ItemActionText();
        }
        else if (_item is FireScroll fireScroll)
        {
            fireScroll.GetMonster(_monster);
            fireScroll.Cast();
            _inventory.RemoveItem(_item.ID);
            _inventoryUi.RefreshInventoryUI();
            itemAction = _item.ItemActionText();
        }
        else if (_item is ShieldScroll shieldScroll)
        {
            shieldScroll.GetPlayer();
            shieldScroll.Cast();
            _inventory.RemoveItem(_item.ID);
            _inventoryUi.RefreshInventoryUI();
            itemAction = _item.ItemActionText();
        }
        else if (_item is Dagger dagger)
        {
            dagger.GetMonster(_monster);
            dagger.Attack();
            itemAction = _item.ItemActionText();
        }
        else if (_item is LongSword longSword)
        {
            longSword.GetMonster(_monster);
            longSword.Attack();
            itemAction = _item.ItemActionText();
        }
        else if (_item is DragonSword dragonSword)
        {
            dragonSword.GetMonster(_monster);
            dragonSword.Attack();
            itemAction = _item.ItemActionText();
        }
        else if (_item is Hammer hammer)
        {
            hammer.GetMonster(_monster);
            hammer.Attack();
            itemAction = _item.ItemActionText();
        }
        else
        {
            Debug.Log("Item not found");
        }

        _itemDetailPanel.SetActive(false); // Close item detail panel after use.

        // If the game HUD is inactive, assume the action is during a battle.
        if (_InGameHud != null && !_InGameHud.activeSelf)
        {
            ItemActionCompleted = true;  // Mark action as completed.
            button.interactable = false; // Disable the button after use.
            _battleManager.GetItemAction(itemAction); // Send action result to BattleManager.
        }
    }

    /// <summary>
    /// Assigns the selected item to this button for interaction.
    /// </summary>
    /// <param name="item">The selected item.</param>
    public void GetItem(Item item)
    {
        if (item != null)
        {
            _item = item;
            Debug.Log($"{_item} is passed well to UseItemButton.");
        }
        else
        {
            Debug.LogWarning("Getting item is null.");
        }
    }

    /// <summary>
    /// Assigns the current monster to this button for interaction during battles.
    /// </summary>
    /// <param name="monster">The monster being interacted with.</param>
    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    /// <summary>
    /// Updates the button text and interactivity based on the selected item type.
    /// </summary>
    /// <param name="item">The selected item to evaluate.</param>
    public void ChangeButtonStatus(Item item)
    {
        TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
        Button button = GetComponent<Button>(); // Get the Button component.

        if (_InGameHud != null && _InGameHud.activeSelf) // If the in-game HUD is active.
        {
            if (item is HealingPotion)
            {
                tmpText.text = "Use Item"; // Update button text for healing items.
                if (button != null)
                {
                    button.interactable = true; // Ensure the button is interactable.
                }
            }
            else
            {
                tmpText.text = "Battle Item"; // Update button text for battle-only items.
                if (button != null)
                {
                    button.interactable = false; // Make the button unclickable for non-healing items.
                }
            }
        }
    }
}
