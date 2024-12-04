using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    // Item prefabs
    public GameObject daggerPrefab;
    public GameObject longSwordPrefab;
    public GameObject dragonSwordPrefab;
    public GameObject healPotionPrefab;
    public GameObject fireScrollPrefab;
    public GameObject shieldScrollPrefab;

    // Inventory list to hold actual item instances
    public List<GameObject> inventory = new List<GameObject>();
    protected List<GameObject> itemsList = new List<GameObject>();

    // Dictionaries to track item quantities
    public Dictionary<int, int> itemQuantities = new Dictionary<int, int>(); // item quantity dic for consumable items


    private void Awake()
    {
        // Clear inventory and quantities for a fresh start
        inventory.Clear();
        itemQuantities.Clear();
    }
    public void SetUp()
    {
        //Add item prefabs in item list
        itemsList.Add(healPotionPrefab);
        itemsList.Add(daggerPrefab);
        itemsList.Add(longSwordPrefab);
        itemsList.Add(dragonSwordPrefab);
        itemsList.Add(fireScrollPrefab);
        itemsList.Add(shieldScrollPrefab);

        //Debug
        //DebugInventoryContents();
    }


    public void AddItem(GameObject itemPrefab)
    {
        if (itemPrefab != null)
        {
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
                    inventory.Add(itemPrefab); // Only add to inventory if it's the first instance
                    Debug.Log(itemComponent);
                    Debug.Log("Item is consumable item and it is new item in inventory");
                }
            }
            else // Item is weapon
            {
                if (!DoesPlayerHave(itemComponent.ID)) // Player doesn't has item in Inventory
                {
                    inventory.Add(itemPrefab);
                    Debug.Log(itemComponent);
                    Debug.Log("New non-consumable item (weapon) added to inventory.");
                }
                else // Player has item in Inventory
                {
                    Debug.Log(itemComponent);
                    Debug.Log("Non-consumable item already exists in inventory, not adding again.");
                }
            }
        }
        else
        {
            Debug.LogWarning("The item prefab does not have an Item component attached!");
        }
        //DebugInventoryContents();
    }

    public void AddRndItem() // add random item in inventory
    {
        int index = Random.Range(1, 101); // 20% dagger, 10% shield scroll, 10% fire scroll, 20% long sword, 10% dragon sword, 30% healing potion
        if (index <= 20)
        {
            AddItem(daggerPrefab);
        }
        else if (index > 20 && index <= 30)
        {
            AddItem(shieldScrollPrefab);
        }
        else if (index > 30 && index <= 40)
        {
            AddItem(fireScrollPrefab);
        }
        else if (index > 40 && index <= 60)
        {
            AddItem(longSwordPrefab);
        }
        else if (index > 60 && index <= 70)
        {
            AddItem(dragonSwordPrefab);
        }
        else if (index > 70)
        {
            AddItem(healPotionPrefab);
        }
    }

    public void RemoveItem(int itemID) // Remove specific item by ID
    {
        // item to remove placeholder
        GameObject itemToRemove = null;
        // Find item in inventory by ID
        foreach (var item in inventory)
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
                // **Change**: Use TryGetValue to safely check and retrieve quantity
                if (itemQuantities.TryGetValue(itemID, out int quantity) && quantity > 0)
                {
                    itemQuantities[itemID]--; // Decrease quantity

                    // Check if quantity is now zero, then remove item from dictionary
                    if (itemQuantities[itemID] == 0)
                    {
                        inventory.Remove(itemToRemove); // Remove item from inventory
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
                inventory.Remove(itemToRemove); // remove item prefab game object in inventory
                Debug.Log($"Removed {itemToRemove} from inventory.");
            }
        }

        else // item is not found
        {
            Debug.Log("Item not found in inventory.");
        }

    }

    public bool DoesPlayerHave(int itemId) // check if the item is stored in inventory by using item id
    {
        foreach (var item in inventory)
        {
            if (item.GetComponent<Item>().ID == itemId)
            {
                Debug.Log("Item exist in inventory");
                return true; // Found the item
            }
        }
        return false; // Item not found
    }

    public void TreasureItemAdd() 
    {
        int index = Random.Range(3, itemsList.Count); 

        GameObject rndItemGameObject = itemsList[index];

        AddItem(rndItemGameObject);
    }

    public void TradeItemAdd() //add dragon sword to inventory 
    {
        AddItem(dragonSwordPrefab);             
    }

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

    public void UseItem(int itemID, Monster monster, Player user) // use item
    {
        if (!DoesPlayerHave(itemID)) // user doesn't have the item
        {
            
            Debug.Log($"You don't have that item in your list.");
            
        }
        else // user have item so use item
        {
            if (itemID == daggerPrefab.GetComponent<Item>().ID) // check item id with dagger id
            {
                daggerPrefab.GetComponent<Weapon>().Attack(monster);
            }
            else if (itemID == longSwordPrefab.GetComponent<Item>().ID)
            {
                longSwordPrefab.GetComponent<Weapon>().Attack(monster);
            }
            else if (itemID == dragonSwordPrefab.GetComponent<Item>().ID)
            {
                dragonSwordPrefab.GetComponent<Weapon>().Attack(monster);
            }
            else if(itemID == healPotionPrefab.GetComponent<Item>().ID)
            {
                healPotionPrefab.GetComponent<HealingPotion>().Heal(user);
                RemoveItem(itemID);
            }
            else if (itemID == fireScrollPrefab.GetComponent<Item>().ID)
            {
                fireScrollPrefab.GetComponent<FireScroll>().Cast(monster);
                RemoveItem(itemID);
            }
            else if (itemID == shieldScrollPrefab.GetComponent<Item>().ID)
            {
                shieldScrollPrefab.GetComponent<ShieldScroll>().Cast(user);
                RemoveItem(itemID);
            }
            else
            {
                Debug.Log("It is not item in the itemsList");               
            }
        }
    }

    public void DebugInventoryContents()
    {
        /*
        Debug.Log("Inventory Contents:");
        if (itemQuantities.Count == 0)
        {
            Debug.Log("The Dic is currently empty.");
            return;
        }

        // Check if the inventory has any items
        if (inventory.Count == 0)
        {
            Debug.Log("The inventory is currently empty.");
            return;
        }
        


        // Show all keys and values in itemQuantities dictionary
        if (itemQuantities.Count == 0)
        {
            Debug.Log("The itemQuantities dictionary is currently empty.");
        }
        else
        {
            Debug.Log("ItemQuantities dictionary contents:");
            foreach (var kvp in itemQuantities)
            {
                Debug.Log($"Item ID: {kvp.Key}, Quantity: {kvp.Value}");
            }
        }
        */
        // Loop through each item in the inventory and print its details
        foreach (var itemObject in inventory)
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
                    Debug.LogError($"Consumable Item: {itemName} (ID: {itemID}), Quantity: {quantity}");
                }
                else
                {
                    Debug.LogError($"Weapon Item: {itemName} (ID: {itemID}), Quantity: 1");
                }
            }
            else
            {
                Debug.LogWarning("Found an item in the inventory without an Item component!");
            }
        }
    }
}

