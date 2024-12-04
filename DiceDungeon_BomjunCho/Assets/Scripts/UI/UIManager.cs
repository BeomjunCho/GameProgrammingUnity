using UnityEngine;

/// <summary>
/// The UIManager class manages different UI layouts in the game, such as Main Menu, In-Game HUD,
/// Pause Menu, Battle Screen, Game Over Screen, How To Play Screen, and Escape Screen.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _Layouts; // Array of UI layouts, indexed by their corresponding MenuLayout enum.

    /// <summary>
    /// Enum representing the different menu layouts in the game.
    /// </summary>
    private enum MenuLayout
    {
        Main = 0,         // Main Menu layout.
        InGame = 1,       // In-Game HUD layout.
        Pause = 2,        // Pause Menu layout.
        Battle = 3,       // Battle Screen layout.
        GameOver = 4,     // Game Over Screen layout.
        HowToPlay = 5,    // How To Play Screen layout.
        PlayerEscape = 6, // Player Escape Screen layout.
    }

    /// <summary>
    /// Sets the initial menu layout to the Main Menu when the game starts.
    /// </summary>
    private void Start()
    {
        OpenMainMenu(); // Open the Main Menu layout by default.
    }

    /// <summary>
    /// Activates the specified layout and deactivates all others.
    /// </summary>
    /// <param name="layout">The layout to activate, specified as a MenuLayout enum.</param>
    private void SetLayout(MenuLayout layout)
    {
        for (int i = 0; i < _Layouts.Length; i++)
        {
            _Layouts[i].SetActive((int)layout == i); // Activate the selected layout and deactivate others.
        }
    }

    /// <summary>
    /// Opens the Main Menu layout.
    /// </summary>
    public void OpenMainMenu()
    {
        SetLayout(MenuLayout.Main);
    }

    /// <summary>
    /// Opens the In-Game HUD layout.
    /// </summary>
    public void OpenInGameHud()
    {
        SetLayout(MenuLayout.InGame);
    }

    /// <summary>
    /// Opens the Pause Menu layout.
    /// </summary>
    public void OpenPauseMenu()
    {
        SetLayout(MenuLayout.Pause);
    }

    /// <summary>
    /// Opens the Battle Screen layout.
    /// </summary>
    public void OpenBattleMenu()
    {
        SetLayout(MenuLayout.Battle);
    }

    /// <summary>
    /// Opens the Game Over Screen layout.
    /// </summary>
    public void OpenGameOverScreen()
    {
        SetLayout(MenuLayout.GameOver);
    }

    /// <summary>
    /// Opens the How To Play Screen layout.
    /// </summary>
    public void OpenHowToPlayScreen()
    {
        SetLayout(MenuLayout.HowToPlay);
    }

    /// <summary>
    /// Opens the Player Escape Screen layout.
    /// </summary>
    public void OpenEscapeScreen()
    {
        SetLayout(MenuLayout.PlayerEscape);
    }
}
