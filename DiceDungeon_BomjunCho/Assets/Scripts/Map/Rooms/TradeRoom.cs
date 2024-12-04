using UnityEngine;

/// <summary>
/// The TradeRoom class represents a room where the player can trade their HP for a Dragon Sword.
/// It handles player entry and exit events, manages room lighting, and initializes trade interactions with the Trade Demon.
/// </summary>
public class TradeRoom : Room
{
    [SerializeField] private TradeDemon _tradeDemon; // The Trade Demon responsible for facilitating trades.

    /// <summary>
    /// Triggered when an object enters the TradeRoom's collider.
    /// Activates the room light, logs the player's entry, and sets up the trade interaction with the Trade Demon.
    /// </summary>
    /// <param name="other">The collider of the object that entered the room.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Trade Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
            _tradeDemon.SetUp(_playerInventory); // Set up trade options for the player.
        }
    }

    /// <summary>
    /// Triggered when an object exits the TradeRoom's collider.
    /// Deactivates the room light and logs that the player has left the room.
    /// </summary>
    /// <param name="other">The collider of the object that exited the room.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Trade Room.");
        }
    }
}
