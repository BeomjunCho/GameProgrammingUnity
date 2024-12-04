using System.Collections;
using UnityEngine;

/// <summary>
/// The PlayerEscape class handles the sequence triggered when the player escapes the game.
/// It includes transitions to the end screen, main menu, and quitting the game.
/// </summary>
public class PlayerEscape : MonoBehaviour
{
    [SerializeField] private GameObject _EscapeImage; // UI image displayed during the escape sequence.
    [SerializeField] private GameObject _EndScreen;   // UI screen displayed after the escape sequence ends.
    [SerializeField] private UIManager _uiSystem;     // Reference to the UIManager for managing UI transitions.

    /// <summary>
    /// Returns the player to the main menu.
    /// </summary>
    public void MainMenu()
    {
        _uiSystem.OpenMainMenu(); // Open the main menu via UIManager.
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit(); // Exit the application.
        Debug.Log("QuitGame called. Application is quitting."); // Debug message for testing.
    }

    /// <summary>
    /// Called when the PlayerEscape is enabled. Starts the escape sequence and unlocks the cursor.
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(EscapeSequence()); // Begin the escape sequence coroutine.
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor for interaction.
        Cursor.visible = true; // Make the cursor visible.
    }

    /// <summary>
    /// Manages the escape sequence, displaying appropriate UI elements and transitioning to the end screen.
    /// </summary>
    /// <returns>IEnumerator for coroutine execution.</returns>
    IEnumerator EscapeSequence()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.PlayerEscape], 10.0f);
        yield return new WaitForSeconds(22);

        _EscapeImage.SetActive(true); // Show the escape image.

        yield return new WaitForSeconds(5); // Wait for 5 seconds.

        _EscapeImage.SetActive(false); // Hide the escape image.
        _EndScreen.SetActive(true); // Show the end screen.
    }
}
