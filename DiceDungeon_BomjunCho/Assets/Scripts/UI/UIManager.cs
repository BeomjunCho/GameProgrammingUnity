using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _Layouts;

    private enum MenuLayout
    {
        Main = 0,
        InGame = 1,
        Pause = 2,
        Battle = 3,
        GameOver = 4,
        HowToPlay = 5,
    }

    private void Start()
    {
        OpenMainMenu();
    }

    private void SetLayout(MenuLayout layout)
    {
        for (int i = 0; i < _Layouts.Length; i++)
        {
            _Layouts[i].SetActive((int)layout == i);
        }
    }

    public void OpenMainMenu()
    {
        SetLayout(MenuLayout.Main);
    }

    public void OpenInGameHud()
    {
        SetLayout(MenuLayout.InGame);
    }

    public void OpenPauseMenu()
    {
        SetLayout(MenuLayout.Pause);
    }

    public void OpenBattleMenu()
    {
        SetLayout(MenuLayout.Battle);
    }
    public void OpenGameOverScreen()
    {
        SetLayout(MenuLayout.GameOver);
    }
    public void OpenHowToPlayScreen() 
    {
        SetLayout(MenuLayout.HowToPlay);
    }
}
