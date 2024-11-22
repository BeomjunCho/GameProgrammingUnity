using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    // Reference to the Inventory script
    private Inventory _inventory;

    // UI slot prefab that represents an inventory slot with item image and quantity
    [SerializeField] private GameObject _slotPrefab;

    // Parent container (the content object of the scroll view)
    [SerializeField] private Transform _contentParent;

    // UI elements for detailed item view
    [SerializeField] private GameObject _itemDetailPanel;
    [SerializeField] private TMP_Text _itemDetailDescription;
    [SerializeField] private UseItemButton _useItemButton;

    // Dictionary to keep track of instantiated slots for updating their quantities
    //private Dictionary<int, GameObject> _instantiatedSlots = new Dictionary<int, GameObject>();

    /// <summary>
    /// Pass inventory in InventoryUI
    /// </summary>
    /// <param name="Inventory"></param>
    public void SetUp(Inventory Inventory)
    {
        _inventory = Inventory;
        _itemDetailPanel.SetActive(false);
    }

    /// <summary>
    /// Reset inventory ui based on inventory instance in game scene.
    /// </summary>
    private void Start()
    {
        if (_inventory != null)
        {
            RefreshInventoryUI();
        }
        else
        {
            Debug.LogWarning("Inventory reference not set in InventoryUI script.");
        }
    }

    /// <summary>
    /// Make item buttons which can show item detail, item sprite, quantity of inventory in inventory panel
    /// Add button on click function for each item buttons 
    /// </summary>
    public void RefreshInventoryUI()
    {
        // Clear any existing UI slots
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }
        //_instantiatedSlots.Clear();

        // Populate the UI with current inventory items
        foreach (var itemObject in _inventory.inventoryList) // Irritate these code for each items in inventory
        {
            Item item = itemObject.GetComponent<Item>(); // Item script
            if (item != null)
            {
                int itemID = item.ID; 
                string itemName = itemObject.name;

                // Instantiate a new slot UI element
                GameObject newSlot = Instantiate(_slotPrefab, _contentParent);                  // Instantiate slot prefab under content parent
                Image itemImage = newSlot.transform.Find("ItemImage").GetComponent<Image>();    // item image in slot prefab
                TMP_Text quantityText = newSlot.transform.Find("TMP_Quantity").GetComponent<TMP_Text>(); // item quantity text in slot prefab

                // Set the image and quantity in the slot
                itemImage.sprite = item.Icon;// Replace with appropriate image source from item in inventory

                if (item is Consumable && _inventory.itemQuantities.ContainsKey(itemID))
                {
                    int quantity = _inventory.itemQuantities[itemID];
                    quantityText.text = quantity > 1 ? quantity.ToString() : ""; // Show quantity only if greater than 1
                }
                else
                {
                    quantityText.text = ""; // No quantity for non-consumable items
                }

                // Add click event listener to the slot button
                Button slotButton = newSlot.GetComponent<Button>(); // get button component from item slot instance
                if (slotButton != null)
                {
                    slotButton.onClick.AddListener(() => { ShowItemDetails(item); PassingItem(item); }); // OnClick -> Show item details and pass item to UseItemButton
                }

                // Store the slot for future updates
                //_instantiatedSlots[itemID] = newSlot;
            }
        }
    }

    /// <summary>
    /// enable item detail panel and shows item description from item itself
    /// </summary>
    /// <param name="item"></param>
    public void ShowItemDetails(Item item)
    {
        if (_itemDetailPanel != null && _itemDetailDescription != null)
        {
            // Display the item's description
            _itemDetailDescription.text = item.description;
            _itemDetailPanel.SetActive(true);
        }

        _useItemButton.ChangeButtonStatus(item);
    }

    /// <summary>
    /// Pass item to UseItemButton script
    /// </summary>
    /// <param name="item"></param>
    public void PassingItem(Item item)
    {
        _useItemButton.GetItem(item);
    }

    /// <summary>
    ///  disable item detail panel
    /// </summary>
    public void CloseItemDetail()
    {
        _itemDetailPanel.SetActive(false);
    }

}

