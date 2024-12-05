using UnityEngine;

/// <summary>
/// The GameOver class handles game-over-related actions, such as quitting the game.
/// </summary>
public class GameOver : MonoBehaviour
{

    [SerializeField] private GameGlobal _gameGlobal; // Reference to the Game global to reset all child when player goes back to menu scene.

    private void OnEnable()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.GameOver], 6.0f);
    }

    public void BackToMainMenu()
    {
        _gameGlobal.ResetGame();

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); // Load the in-game scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
