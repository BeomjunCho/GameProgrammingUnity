using UnityEngine;

/// <summary>
/// The PauseMenu class manages the behavior of the pause menu, including resuming the game
/// and quitting the application. It also handles UI transitions and game state changes when paused.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIManager _uiSystem;      // Reference to the UIManager for screen transitions.
    [SerializeField] private InGameHud _ingameHud;     // Reference to the InGameHud for managing in-game UI state.
    [SerializeField] private HowToPlay _howToPlay;     // Reference to the HowToPlay menu for knowing that it is opened from pause menu.

    /// <summary>
    /// Handles input for resuming the game when the Tab key is pressed.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _uiSystem.OpenInGameHud(); // Switch back to the in-game HUD.
            _ingameHud.gamePaused = false; // Resume the game.
        }
    }

    /// <summary>
    /// Called when the pause menu is enabled. Stops the game time, unlocks the cursor, and pauses the game.
    /// </summary>
    private void OnEnable()
    {
        AudioManager.Instance.StopMusic();       // Stop the currently playing music.
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor for interaction.
        Cursor.visible = true;                   // Make the cursor visible.
        Time.timeScale = 0f;                     // Pause the game time.
    }

    /// <summary>
    /// Called when the pause menu is disabled. Resumes the game time.
    /// </summary>
    private void OnDisable()
    {
        Time.timeScale = 1.0f; // Resume the game time.
    }

    /// <summary>
    /// Resumes the game when the "Continue" button is pressed.
    /// </summary>
    public void ButtonContinue()
    {
        _uiSystem.OpenInGameHud(); // Switch back to the in-game HUD.
        _ingameHud.gamePaused = false; // Resume the game.
    }

    /// <summary>
    /// Quits the game when the "Quit" button is pressed.
    /// </summary>
    public void ButtonQuitGame()
    {
        Application.Quit(); // Exit the application.
        Debug.Log("QuitGame called. Application is quitting."); // Log for debugging purposes.
    }

    /// <summary>
    /// Opens the "How to Play" screen.
    /// </summary>
    public void ButtonHowToplay()
    {
        _howToPlay.fromPaused = true;
        _uiSystem.OpenHowToPlayScreen(); // Transition to the "How to Play" screen.
        
    }
}
