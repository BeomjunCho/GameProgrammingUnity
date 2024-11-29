using UnityEngine;

/// <summary>
/// The TreasureRoom class represents a room where the player can receive random items as rewards.
/// It handles player entry and exit events, toggling room lights and logging relevant information.
/// </summary>
public class TreasureRoom : Room
{
    /// <summary>
    /// Triggered when an object enters the TreasureRoom's collider.
    /// Activates the room light and logs that the player has entered the room.
    /// </summary>
    /// <param name="other">The collider of the object that entered the room.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Treasure Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
        }
    }

    /// <summary>
    /// Triggered when an object exits the TreasureRoom's collider.
    /// Deactivates the room light and logs that the player has left the room.
    /// </summary>
    /// <param name="other">The collider of the object that exited the room.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Treasure Room.");
        }
    }
}
