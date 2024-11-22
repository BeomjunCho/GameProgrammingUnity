using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIManager _uiSystem;
    [SerializeField] private InGameHud _ingameHud;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private UseItemButton _useItemButton;

    private Inventory _inventory;


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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _uiSystem.OpenInGameHud();
        }
    }

    private void OnEnable()
    {
        AudioManager.Instance.StopMusic();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _inventoryUI.RefreshInventoryUI();
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        _inventoryUI.CloseItemDetail();
        Time.timeScale = 1.0f;
    }
}
