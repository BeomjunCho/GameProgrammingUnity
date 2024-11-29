using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// The BattleManager class handles the turn-based battle system between the player and a monster.
/// It manages health updates, turn progression, and the end of battle conditions.
/// </summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField] private UseItemButton _useItemButton;
    [SerializeField] private TMP_Text _battleProgress;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _RunAwayButton;
    [SerializeField] private BattleHud _battleHud;

    private Player _player;
    private Interaction _interaction;
    private Monster _monster;
    private string _itemActionText;

    /// <summary>
    /// Initializes the BattleManager with the player's information.
    /// </summary>
    public void SetUp(Player player)
    {
        _player = player;
        _interaction = _player.gameObject.GetComponent<Interaction>();
    }

    /// <summary>
    /// Called when the BattleManager is enabled. Sets up the item button.
    /// </summary>
    private void OnEnable()
    {
        _useItemButton.SetUp();
    }

    /// <summary>
    /// Assigns the current monster from battleHud to the BattleManager.
    /// </summary>
    /// <param name="monster">The monster involved in the battle.</param>
    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    /// <summary>
    /// Updates the item action text after the player uses an item.
    /// </summary>
    /// <param name="itemActionText">The outcome text of the item action.</param>
    public void GetItemAction(string itemActionText)
    {
        _itemActionText = itemActionText;
    }

    /// <summary>
    /// Starts the battle, setting up the player and monster HUD and initiating the player's turn.
    /// </summary>
    public void StartBattle()
    {
        //Update player health and monster health UI
        _battleHud.OnHealthChange(_player.curHP, _player.maxHp, _player.shield, true);
        _battleHud.OnHealthChange(_monster.cur_hp, _monster.max_hp, 0, false);

        _useItemButton.ItemActionCompleted = false;
        _battleProgress.color = Color.black;
        _battleProgress.text = "Choose one item to use.";
        _interaction.isPlayerFighting = true;

        Room battleRoom = _interaction.currentBattleRoom;
        if (battleRoom != null && battleRoom is CombatRoom combatRoom)
        {
            combatRoom.ReadyToBattle();
        }
        else if (battleRoom != null && battleRoom is BossRoom bossRoom)
        {
            bossRoom.ReadyToBattle();
        }
        else
        {
            Debug.LogWarning("Battle room is null in battle manager.");
        }
        //Start player turn
        StartCoroutine(PlayerTurn());
    }


    /// <summary>
    /// Handles the player's turn in the battle, waiting for an item action and resolving its effects.
    /// </summary>
    IEnumerator PlayerTurn()
    {
        _RunAwayButton.SetActive(true);
        Debug.Log("Player's turn started.");

        _battleProgress.color = Color.black;
        _battleProgress.text = "Choose one item to use.";

        yield return new WaitUntil(() => _useItemButton.ItemActionCompleted == true);

        _RunAwayButton.SetActive(false);
        _battleProgress.color = Color.blue;
        _battleProgress.text = "Rolling dice...";

        yield return new WaitForSeconds(2f);

        _battleProgress.text = _itemActionText;
        _useItemButton.button.interactable = false;

        //Update player health and monster health UI
        _battleHud.OnHealthChange(_player.curHP, _player.maxHp, _player.shield, true);
        _battleHud.OnHealthChange(_monster.cur_hp, _monster.max_hp, 0, false);

        yield return new WaitForSeconds(3f);

        if (_monster.cur_hp <= 0)
        {
            // Disable monster
            if (_monster is BossMonster bossMonster)
            {
                bossMonster.BossMonsterDead();
            }
            else if (_monster is NormalMonster normalMonster)
            {
                normalMonster.MonsterDead();
            }
            yield return new WaitForSeconds(1f);

            _player.LevelUp();
            EndBattle();

            _useItemButton.ItemActionCompleted = false;
            _useItemButton.button.interactable = true;
            
            _uiManager.OpenInGameHud();

            yield break; // Stop further actions
        }

        StartCoroutine(MonsterTurn());
    }

    /// <summary>
    /// Handles the monster's turn, resolving its attack and checking if the player survives.
    /// </summary>
    IEnumerator MonsterTurn()
    {
        Debug.Log("Monster's turn started.");

        yield return new WaitForSeconds(1f);

        _battleProgress.color = Color.red;
        _battleProgress.text = "Rolling dice...";

        yield return new WaitForSeconds(2f);

        // Attack player and return proper text
        _battleProgress.text = _monster.Attack();

        //Update player health and monster health UI
        _battleHud.OnHealthChange(_player.curHP, _player.maxHp, _player.shield, true);
        _battleHud.OnHealthChange(_monster.cur_hp, _monster.max_hp, 0, false);

        yield return new WaitForSeconds(3f);

        //Use item button interactable for next player turn
        _useItemButton.ItemActionCompleted = false;
        _useItemButton.button.interactable = true;

        if (_player.curHP <= 0)
        {
            Debug.Log("Player defeated. Game Over.");
            EndBattle();
            _uiManager.OpenGameOverScreen();
            yield break; // Stop further actions
        }
        StartCoroutine(PlayerTurn());
    }

    /// <summary>
    /// Ends the battle, resetting player state and moving the player to a safe position.
    /// </summary>
    public void EndBattle()
    {
        Debug.Log("Battle ended.");
        _interaction.isPlayerFighting = false;

        Room currentBattleRoom = _interaction.currentBattleRoom;
       
        if (currentBattleRoom is CombatRoom combatRoom)
        {
            combatRoom.MovePlayerToEndBattlePos();
        }      
        else if (currentBattleRoom is BossRoom bossRoom)
        {
            bossRoom.MovePlayerToEndBattlePos();
        }
    }
}

