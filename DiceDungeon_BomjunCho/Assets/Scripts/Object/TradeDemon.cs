using UnityEngine;
using TMPro;
using System.Collections;

/// <summary>
/// The TradeDemon class handles interactions where the player can trade specific items
/// with the demon to receive a Dragon Sword. It includes trade success and failure behavior.
/// </summary>
public class TradeDemon : MonoBehaviour
{
    [SerializeField] private GameObject _dragonSword; // Prefab for the Dragon Sword to be given.
    [SerializeField] private GameObject _spawnPosition; // Position where the Dragon Sword will spawn.
    [SerializeField] private GameObject _Deco; // Decorative Dragon Sword displayed before the trade.
    [SerializeField] private float _throwForce; // Force applied when throwing the Dragon Sword.

    private bool _isTrade = false; // Ensures trading can only happen once.
    private Inventory _inventory; // Reference to the player's inventory.
    private TextMeshProUGUI _inGameText; // UI text for displaying trade messages.
    private GameObject _InGameTextPanel; // Panel for displaying the in-game text.

    /// <summary>
    /// Sets up the demon for trading by initializing the inventory and UI elements.
    /// </summary>
    /// <param name="inventory">The player's inventory, used for checking and removing items.</param>
    public void SetUp(Inventory inventory)
    {
        _inventory = inventory;
        if (_inventory != null)
        {
            // Locate the in-game text UI elements.
            _inGameText = GameObject.Find("UI_Manager/InGameHud/InGameTextPanel/InGameText").GetComponent<TextMeshProUGUI>();
            _InGameTextPanel = GameObject.Find("UI_Manager/InGameHud/InGameTextPanel");
        }
    }

    /// <summary>
    /// Initiates the trade process. If the player has the required items, the trade succeeds; otherwise, it fails.
    /// </summary>
    public void TradeWithDemon()
    {
        if (_isTrade) return; // Prevent repeated trades.

        if (_inventory.DoesPlayerHave(1) && _inventory.DoesPlayerHave(5)) // Check for required items.(fire scroll and dagger)
        {
            StartCoroutine(TradeItem()); // Start trade success sequence.
        }
        else
        {
            StartCoroutine(TradeFail()); // Start trade failure sequence.
        }
    }

    /// <summary>
    /// Handles the successful trade sequence, where the player receives the Dragon Sword and loses the required items.
    /// </summary>
    IEnumerator TradeItem()
    {
        // Display success message.
        _InGameTextPanel.SetActive(true);
        _inGameText.text = "You have items I need! Thank you for trading!";

        yield return new WaitForSeconds(3f); // Wait for 3 seconds.

        // Destroy the decorative Dragon Sword to imitate the trade.
        Destroy(_Deco);

        // Spawn and throw the Dragon Sword.
        GameObject thrownItem = Instantiate(_dragonSword, _spawnPosition.transform.position, Quaternion.identity);
        Rigidbody rb = thrownItem.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * _throwForce + transform.right * (_throwForce / 2) * -1, ForceMode.Impulse);

        // Remove the required items from the player's inventory.
        _inventory.RemoveItem(1); // Remove Dagger.
        _inventory.RemoveItem(5); // Remove Fire Scroll.

        // Prevent further trades.
        _isTrade = true;

        // Update UI to indicate item loss.
        _inGameText.text = "Player lost 1 dagger and 1 fire scroll.";
        yield return new WaitForSeconds(3f); // Wait for 3 seconds.

        // Hide the text panel.
        _InGameTextPanel.SetActive(false);
    }

    /// <summary>
    /// Handles the failed trade sequence, displaying a message to the player about missing items.
    /// </summary>
    IEnumerator TradeFail()
    {
        // Display failure message.
        _InGameTextPanel.SetActive(true);
        _inGameText.text = "You don't have items I need. I need 1 dagger and 1 fire scroll" +
            "\nFind them and come back here. Then, I will give this dragon sword.";

        yield return new WaitForSeconds(5f); // Wait for 5 seconds.

        // Clear and hide the text panel.
        _inGameText.text = "";
        _InGameTextPanel.SetActive(false);
    }
}
