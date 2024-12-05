using UnityEngine;

/// <summary>
/// The GameOver class handles game-over-related actions, such as quitting the game.
/// </summary>
public class GameOver : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.GameOver], 6.0f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
