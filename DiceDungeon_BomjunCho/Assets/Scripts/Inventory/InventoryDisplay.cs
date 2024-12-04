using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject itemSlotPrefab;    // Prefab for each item slot
    public GameObject PlayerStatusText;      // TMPro for update player status
    public Transform contentPanel;       // Reference to the Content object within the Scroll View
    public GameObject inventoryPanel;      // Reference to the main inventory UI panel

    public Inventory _playerInventory;

    private void Start()
    {
        GameObject inventory = transform.Find("Inventory").gameObject;
        if (inventory == null)
        {
            Debug.LogError("Fail to find Inventory GameObject");
        }
        _playerInventory = inventory.GetComponent<Inventory>();
        inventoryPanel.SetActive(false); // Start with the inventory hidden
        PlayerStatusText.SetActive(false); // Start with the player status hidden
    }


    private void Update()
    {
        // Toggle inventory visibility on Tab key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryDisplay();
        }
    }

    private void ToggleInventoryDisplay()
    {
        // Toggle the active state of the inventory panel
        bool isActive = !inventoryPanel.activeSelf; // check active state of panel and reverse it to use in SetActive
        inventoryPanel.SetActive(isActive); // Inventory panel active
        PlayerStatusText.SetActive(isActive); // Player status active

        // Refresh the inventory display if making it visible
        if (isActive)
        {
            DisplayInventory(_playerInventory);
        }
    }

    public void DisplayInventory(Inventory _playerInventory)
    {
        // Clear existing items in the content panel
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Populate the scroll view grid with inventory items
        foreach (GameObject itemPrefab in _playerInventory.inventory)
        {
            Item itemComponent = itemPrefab.GetComponent<Item>();
            if (itemComponent == null) continue;

            // Instantiate a new item slot placeholder in the content panel
            GameObject itemSlot = Instantiate(itemSlotPrefab, contentPanel);

            // Change placeholder icon to proper icon from item prefab
            itemSlot.transform.Find("Icon").GetComponent<Image>().sprite = itemComponent.Icon;

            // Display quantity
            if (itemComponent is Consumable)
            {
                // Call Item quantity from Inventory InventoryInstance and replace placeholer quantity to proper one
                itemSlot.transform.Find("Quantity").GetComponent<TMP_Text>().text =
                _playerInventory.itemQuantities[itemComponent.ID].ToString();
            }
            else // item is weapon
            {
                itemSlot.transform.Find("Quantity").GetComponent<TMP_Text>().text = "1"; // Always 1
            }
        }
        string playerName = PlayerStatusText.GetComponent<Player>().userName.ToString();
        string playerHp = PlayerStatusText.GetComponent<Player>().hp.ToString();
        string playerShield = PlayerStatusText.GetComponent<Player>().shield.ToString();

        PlayerStatusText.transform.GetComponent<TMP_Text>().text = $"Name : {playerName}\nHealth Point : {playerHp}\nShield : {playerShield}";

    }
}
