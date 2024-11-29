using UnityEngine;

/// <summary>
/// The GameOver class handles game-over-related actions, such as quitting the game.
/// </summary>
public class GameOver : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
}
