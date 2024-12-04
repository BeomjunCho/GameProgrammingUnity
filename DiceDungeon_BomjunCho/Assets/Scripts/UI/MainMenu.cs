using UnityEngine;

/// <summary>
/// The MainMenu class manages the main menu screen, allowing the player to start the game, view the "How to Play" screen, or quit the game.
/// </summary>
public class MainMenu : MonoBehaviour
{

    [SerializeField] private UIManager _uiSystem;
    [SerializeField] private InGameHud _gameHud;
    [SerializeField] private GameController _gameController;

    /// <summary>
    /// Plays the main menu music when the main menu is loaded.
    /// </summary>
    private void Start()
    {
        if (AudioManager.Instance.musicList.Length > (int)MusicTrack.MainMenuMusic)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.musicList[(int)MusicTrack.MainMenuMusic], 1.5f);
        }
    }

    /// <summary>
    /// Starts the game, loading the in-game scene, initializing the game state, and setting up the in-game UI.
    /// </summary>
    public void ButtonStartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGameScene"); // Load the in-game scene.
        _gameController.StartGame();                                       // Start the game via the GameController.
        _uiSystem.OpenInGameHud();                                         // Transition to the in-game HUD.
        _gameHud.SetUp();                                                 // Set up the in-game HUD.

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor for gameplay.
        Cursor.visible = false;                  // Hide the cursor.
    }

    /// <summary>
    /// Opens the "How to Play" screen.
    /// </summary>
    public void ButtonHowToplay()
    {
        _uiSystem.OpenHowToPlayScreen(); // Transition to the "How to Play" screen.
    }

    /// <summary>
    /// Quits the game application.
    /// </summary>
    public void ButtonQuitGame()
    {
        Application.Quit(); // Exit the application.
        Debug.Log("QuitGame called. Application is quitting."); // Log for debugging (won't appear in a built game).
    }
}
