using UnityEngine;

/// <summary>
/// The StartingRoom class represents the room where the player begins the game.
/// It manages player entry and exit events, toggling room lights and logging relevant information.
/// </summary>
public class StartingRoom : Room
{
    /// <summary>
    /// Triggered when an object enters the StartingRoom's collider.
    /// Activates the room light and logs that the player has entered the room.
    /// </summary>
    /// <param name="other">The collider of the object that entered the room.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Starting Room.");
            RoomLight.SetActive(true);
            Debug.Log("Light on");
        }
    }

    /// <summary>
    /// Triggered when an object exits the StartingRoom's collider.
    /// Deactivates the room light and logs that the player has left the room.
    /// </summary>
    /// <param name="other">The collider of the object that exited the room.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomLight.SetActive(false);
            Debug.Log("Light off");
            Debug.Log("Player is leaving from Starting Room.");
        }
    }
}
