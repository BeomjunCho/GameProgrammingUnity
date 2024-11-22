using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseItemButton : MonoBehaviour
{
    [SerializeField] private InventoryUI _inventoryUi;
    [SerializeField] private GameObject _itemDetailPanel;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private BattleManager _battleManager;  
    private Inventory _inventory;
    private Item _item;
    private Monster _monster;
    public Button button;
    public bool ItemActionCompleted = false;
    public void SetUp(Inventory inventory)
    {
        _inventory = inventory;
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        ItemActionCompleted = false;
        button.interactable = true;
    }
    public void UseItem()
    {
        string itemAction = "";
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
        else
        {
            Debug.Log("Item not found");
        }

        if (!_inventory.DoesPlayerHave(4) || !_inventory.DoesPlayerHave(5) || !_inventory.DoesPlayerHave(6))
        {
            _itemDetailPanel.SetActive(false);
        }

        if (_pauseMenu != null && !_pauseMenu.activeSelf)
        {
            ItemActionCompleted = true;
            button.interactable = false;
            _battleManager.GetItemAction(itemAction);
        }

    }

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


    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    public void ChangeButtonStatus(Item item)
    {
        TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
        Button button = GetComponent<Button>(); // Get the Button component

        if (_pauseMenu != null && _pauseMenu.activeSelf)
        {
            if (item is HealingPotion)
            {
                tmpText.text = "Use Item";
                if (button != null)
                {
                    button.interactable = true; // Ensure the button is interactable
                }
            }
            else
            {
                tmpText.text = "Battle Item";
                if (button != null)
                {
                    button.interactable = false; // Make the button unclickable
                }
            }
        }
    }
}
