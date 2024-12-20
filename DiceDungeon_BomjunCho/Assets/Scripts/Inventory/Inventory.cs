﻿using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    // Item prefabs
    public GameObject daggerPrefab;           // Prefab for the dagger weapon
    public GameObject longSwordPrefab;        // Prefab for the long sword weapon
    public GameObject dragonSwordPrefab;      // Prefab for the dragon sword weapon
    public GameObject healPotionPrefab;       // Prefab for the healing potion
    public GameObject fireScrollPrefab;       // Prefab for the fire scroll
    public GameObject shieldScrollPrefab;     // Prefab for the shield scroll
    public GameObject HammerPrefab;                 // Prefab for the Hammer

    // Inventory list to hold actual item instances
    public List<GameObject> inventoryList = new List<GameObject>(); // List of all items in the inventory
    protected List<GameObject> itemsList = new List<GameObject>();  // List of all available item prefabs

    // Dictionaries to track item quantities
    public Dictionary<int, int> itemQuantities = new Dictionary<int, int>(); // item quantity dic for consumable items

    /// <summary>
    /// Clears the inventory and resets item quantities on initialization.
    /// </summary>
    private void Awake()
    {
        // Clear inventory and quantities for a fresh start
        inventoryList.Clear();
        itemQuantities.Clear();
    }

    /// <summary>
    /// Sets up the inventory by populating the available item prefab list.
    /// </summary>
    public void SetUp()
    {
        //Add item prefabs in item list
        itemsList.Add(healPotionPrefab);
        itemsList.Add(daggerPrefab);
        itemsList.Add(longSwordPrefab);
        itemsList.Add(dragonSwordPrefab);
        itemsList.Add(fireScrollPrefab);
        itemsList.Add(shieldScrollPrefab);
        itemsList.Add(HammerPrefab);
    }

    /// <summary>
    /// Adds an item to the inventory based on its ID.
    /// Handles both consumables and weapons differently.
    /// </summary>
    /// <param name="itemId">The ID of the item to add.</param>
    public void AddItem(int itemId)
    {
        GameObject itemPrefab = FindItemPrefabByID(itemId);
        Item itemComponent = itemPrefab.GetComponent<Item>();

        if (itemComponent is Consumable)
        {
            if (!itemQuantities.ContainsKey(itemComponent.ID))
            {
                itemQuantities.Add(itemComponent.ID, 0);
            }
            // Track consumable quantities
            if (DoesPlayerHave(itemComponent.ID)) // Player has item
            {
                itemQuantities[itemComponent.ID]++; // Quantity + 1
                Debug.Log(itemComponent);
                Debug.Log("Item is consumable item and it's quantity +1");
            }
            else // Player doesn't has item in Inventory
            {
                itemQuantities[itemComponent.ID] = 1; // Initialize item quantity
                inventoryList.Add(itemPrefab); // Only add to inventory if it's the first instance
                Debug.Log(itemComponent);
                Debug.Log("Item is consumable item and it is new item in inventory");
            }
        }
        else if (itemComponent is Weapon) // Item is weapon
        {
            if (!DoesPlayerHave(itemComponent.ID)) // Player doesn't has item in Inventory
            {
                inventoryList.Add(itemPrefab);
                Debug.Log(itemComponent);
                Debug.Log("New non-consumable item (weapon) added to inventory.");
            }
            else // Player has item in Inventory
            {
                Debug.Log(itemComponent);
                Debug.Log("Non-consumable item already exists in inventory, not adding again.");
            }
        }
        
        DebugInventoryContents();
    }

    /// <summary>
    /// Removes an item from the inventory based on its ID.
    /// Handles quantity for consumables and complete removal for weapons.
    /// </summary>
    /// <param name="itemID">The ID of the item to remove.</param>
    public void RemoveItem(int itemID) // Remove specific item by ID
    {
        // item to remove placeholder
        GameObject itemToRemove = null;
        // Find item in inventory by ID
        foreach (var item in inventoryList)
        {
            if (item.GetComponent<Item>().ID == itemID)
            {
                itemToRemove = item; // find item which is needed to removed
                break;
            }
        }
        
        if (itemToRemove != null) // item is found
        {
            Item item = itemToRemove.GetComponent<Item>(); //call item script
            if (item is Consumable)
            {
                // Use TryGetValue to safely check and retrieve quantity
                if (itemQuantities.TryGetValue(itemID, out int quantity) && quantity > 0)
                {
                    itemQuantities[itemID]--; // Decrease quantity

                    // Check if quantity is now zero, then remove item from dictionary
                    if (itemQuantities[itemID] == 0)
                    {
                        inventoryList.Remove(itemToRemove); // Remove item from inventory
                        itemQuantities.Remove(itemID); // Remove ID and value from dictionary
                        Debug.Log($"Removed {itemToRemove.name} completely from inventory (quantity reached zero).");
                    }
                    else // quantity is not 0
                    {
                        Debug.Log($"Used one {itemToRemove.name}. Remaining quantity: {itemQuantities[itemID]}.");
                    }
                }
                else // There is no cunsumable item in inventory
                {
                    Debug.LogWarning($"No {itemToRemove.name} left in inventory to remove.");
                }
            }
            if (item is Weapon) 
            {
                inventoryList.Remove(itemToRemove); // remove item prefab game object in inventory
                Debug.Log($"Removed {itemToRemove} from inventory.");
            }
        }

        else // item is not found
        {
            Debug.Log("Item not found in inventory.");
        }

    }

    /// <summary>
    /// Checks if the player already has an item in the inventory based on its ID.
    /// </summary>
    /// <param name="itemId">The ID of the item to check.</param>
    /// <returns>True if the item exists in the inventory, otherwise false.</returns>
    public bool DoesPlayerHave(int itemId) // check if the item is stored in inventory by using item id
    {
        foreach (var item in inventoryList)
        {
            if (item.GetComponent<Item>().ID == itemId)
            {
                Debug.Log("Item exist in inventory");
                return true; // Found the item
            }
        }
        return false; // Item not found
    }

    /// <summary>
    /// Finds an item prefab by its ID from the available item list.
    /// </summary>
    /// <param name="itemID">The ID of the item to find.</param>
    /// <returns>The item prefab if found, otherwise null.</returns>
    public GameObject FindItemPrefabByID(int itemID)
    {
        foreach (var prefab in itemsList)
        {
            Item itemComponent = prefab.GetComponent<Item>();
            if (itemComponent != null && itemComponent.ID == itemID)
            {
                return prefab; // Return the matching prefab
            }
        }
        Debug.LogWarning($"No matching prefab found for item ID: {itemID}");
        return null; // Return null if no matching prefab is found
    }

    /// <summary>
    /// Prints the contents of the inventory to the debug console.
    /// Includes item names, IDs, and quantities.
    /// </summary>
    public void DebugInventoryContents()
    {
        Debug.Log("*************Inventory contents*************");
        // Loop through each item in the inventory and print its details
        foreach (var itemObject in inventoryList)
        {
            Item item = itemObject.GetComponent<Item>();

            // Check if the item has been properly added to inventory
            if (item != null)
            {
                // Get item information
                int itemID = item.ID;
                string itemName = itemObject.name;

                // Print item details
                if (item is Consumable && itemQuantities.ContainsKey(itemID))
                {
                    int quantity = itemQuantities[itemID];
                    Debug.Log($"Consumable Item: {itemName} (ID: {itemID}), Quantity: {quantity}");
                }
                else
                {
                    Debug.Log($"Weapon Item: {itemName} (ID: {itemID}), Quantity: 1");
                }
            }
            else
            {
                Debug.LogWarning("Found an item in the inventory without an Item component!");
            }
        }
        Debug.Log("*********************************************");
    }
}

