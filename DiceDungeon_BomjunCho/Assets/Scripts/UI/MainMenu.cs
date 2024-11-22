using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private InGameHud _gameHud;
    [SerializeField] private GameController _gameController;

    private void Start()
    {
        if (AudioManager.Instance.musicList.Length > (int)MusicTrack.MainMenuMusic)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.musicList[(int)MusicTrack.MainMenuMusic], 1.5f);
        }
    }
    public void ButtonStartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGameScene");
        _gameController.StartGame();
        _uiManager.OpenInGameHud();
        _gameHud.OnStartGame();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ButtonHowToplay()
    {
        _uiManager.OpenHowToPlayScreen();
    }

    public void ButtonQuitGame()
    {
        Application.Quit();
    }
}
