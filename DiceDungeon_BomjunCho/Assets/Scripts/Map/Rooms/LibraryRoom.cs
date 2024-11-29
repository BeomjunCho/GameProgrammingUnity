using UnityEngine;

/// <summary>
/// The LibraryRoom class represents a room where the player can obtain scrolls based on their luck.
/// It manages player entry and exit events, toggling room lights and logging relevant information.
/// </summary>
public class LibraryRoom : Room
{
    /// <summary>
    /// Triggered when an object enters the LibraryRoom's collider.
    /// Activates the room light and logs that the player has entered the room.
    /// </summary>
    /// <param name="other">The collider of the object that entered the room.</param>
    private void OnTriggerEnter(Collider other) // Trigger execute with player tag
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the Library Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
        }
    }

    /// <summary>
    /// Triggered when an object exits the LibraryRoom's collider.
    /// Deactivates the room light and logs that the player has left the room.
    /// </summary>
    /// <param name="other">The collider of the object that exited the room.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Library Room.");
        }
    }
}




