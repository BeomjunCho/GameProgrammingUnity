using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour
{
    [SerializeField] private UIManager _uiSystem;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private BattleHud _battleHud;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _playerHealth;
    private Player _player;

    private bool _gamePaused = true;
    private float _timerTime = 0;

    private void Start()
    {
        _timer.text = "Timer Paused";
        _timer.color = Color.yellow;
    }

    public void OnStartGame()
    {
        _gamePaused = false;
        _healthBar.fillAmount = 1;
        _pauseMenu.SetUp();
        _player = Object.FindAnyObjectByType<Player>();
        OnHealthChange(_player.curHP, _player.maxHp);
        _battleHud.SetUp();
    }

    private void OnEnable()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicList[(int)MusicTrack.InGameMusic], 0.5f);
        //AudioManager.Instance.PlayAmbience(AudioManager.Instance.ambience.clip, 0.5f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_gamePaused)
            return;
        _timerTime += Time.deltaTime;
        _timer.text = $"{_timerTime,0:0.000}";

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PauseMenu();
        }

        OnHealthChange(_player.curHP, _player.maxHp);
    }

    public void OnHealthChange(float currenthealth, float maxHealth)
    {
        _healthBar.fillAmount = currenthealth / maxHealth;
        _playerHealth.text = $"{currenthealth} / {maxHealth}";
    }

    private void PauseMenu()
    {
        _uiSystem.OpenPauseMenu();
    }


}
