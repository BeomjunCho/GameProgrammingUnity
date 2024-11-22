using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BattleHud : MonoBehaviour
{
    private Player _player;   
    private Interaction _interaction;
    private PlayerController _playerController;

    [SerializeField] private UIManager _uiSystem;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private UseItemButton _useItemButton;
    [SerializeField] private GameObject _itemDiscription;
    [SerializeField] private BattleManager _battleManager;

    [SerializeField] private Image _currentHealth_P;
    [SerializeField] private Image _currentHealth_M;
    [SerializeField] private TMP_Text _playerMaxHealth;
    [SerializeField] private TMP_Text _monsterMaxHealth;
    public Monster _monster;
    private Inventory _inventory;
    public void SetUp()
    {
        // FInd Inventory in game scene.
        _inventory = Object.FindAnyObjectByType<Inventory>();
        if (_inventory != null)
        {
            _inventoryUI.SetUp(_inventory);
            _useItemButton.SetUp(_inventory);
        }
        else
        {
            Debug.LogWarning("Inventory is not set correctly.");
        }
        _player = Object.FindAnyObjectByType<Player>();
        _battleManager.SetUp(_player);
    }

    private void Awake()
    {
        if (_interaction == null)
        {
            _interaction = Object.FindAnyObjectByType<Interaction>();
            Debug.Log("Interaction assigned in BattleHud: " + _interaction);
            _playerController = Object.FindAnyObjectByType<PlayerController>();
            Debug.Log("PlayerController assigned in BattleHud: " + _playerController);
        }
    }


    public void OnHealthChange(float currentHealth, float maxHealth, float shield, bool isPlayer = true)
    {
        if (isPlayer)
        {
            _currentHealth_P.fillAmount = currentHealth / maxHealth;
            _playerMaxHealth.text = $"{currentHealth}/{maxHealth} / shield: {shield}";
        }
        else
        {
            _currentHealth_M.fillAmount = currentHealth / maxHealth;
            _monsterMaxHealth.text = $"{currentHealth}/{maxHealth}";
        }
    }

    private void OnEnable()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic(AudioManager.Instance.musicList[(int)MusicTrack.BattleMusic], 0.8f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _playerController.canMove = false;

        _monster = _interaction.ReturnDetectedMonster();
        _useItemButton.GetMonster(_monster);
        _battleManager.GetMonster(_monster);

        _inventoryUI.RefreshInventoryUI();

        _battleManager.StartBattle();
    }

    private void OnDisable()
    {
        _playerController.canMove = true;
        _itemDiscription.SetActive(false);
    }

    private void Update()
    {   
        OnHealthChange(_player.curHP, _player.maxHp, _player.shield, true);
        OnHealthChange(_monster.cur_hp, _monster.max_hp, 0,  false);
    }
}
