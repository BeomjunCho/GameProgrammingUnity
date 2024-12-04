using UnityEngine;

/// <summary>
/// The HealingRoom class represents a room where the player can be healed.
/// It manages player entry and exit events, toggling room lights and logging relevant information.
/// </summary>
public class HealingRoom : Room 
{
    /// <summary>
    /// Triggered when an object enters the HealingRoom's collider.
    /// Activates the room light and logs that the player has entered the room.
    /// </summary>
    /// <param name="other">The collider of the object that entered the room.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Healing Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
        }
    }

    /// <summary>
    /// Triggered when an object exits the HealingRoom's collider.
    /// Deactivates the room light and logs that the player has left the room.
    /// </summary>
    /// <param name="other">The collider of the object that exited the room.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Healing Room.");
        }
    }

}




