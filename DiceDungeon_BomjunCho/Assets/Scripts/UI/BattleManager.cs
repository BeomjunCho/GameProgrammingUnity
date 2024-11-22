using System.Collections;
using TMPro;
using UnityEngine;


public class BattleManager : MonoBehaviour
{
    private Player _player;
    private Monster _monster;
    private string _itemActionText;
    [SerializeField] private UseItemButton _useItemButton;
    [SerializeField] private TMP_Text _battleProgress;
    [SerializeField] private UIManager _uiManager;

    public void SetUp(Player player)
    {
        _player = player;
        Debug.Log("Battle manager player set");
    }

    public void GetMonster(Monster monster)
    {
        _monster = monster;
        Debug.Log("Battle manager monster set");
    }

    public void StartBattle()
    {
        _useItemButton.ItemActionCompleted = false;
        _battleProgress.text = "Choose one item to use.";
        StartCoroutine(PlayerTurn());
    }

    public void GetItemAction(string itemActionText)
    {
        _itemActionText = itemActionText;
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Player's turn started.");

        yield return new WaitUntil(() => _useItemButton.ItemActionCompleted == true);

        _battleProgress.text = _itemActionText;

        yield return new WaitForSeconds(3f);

        if (_monster.cur_hp <= 0)
        {
            Debug.Log("Killed monster!");
            _useItemButton.ItemActionCompleted = false;
            _useItemButton.button.interactable = true;
            _uiManager.OpenInGameHud();
            yield break; // Stop further actions
        }

        StartCoroutine(MonsterTurn());
    }

    IEnumerator MonsterTurn()
    {
        Debug.Log("Monster's turn started.");

        yield return new WaitForSeconds(1f);

        _battleProgress.text = _monster.Attack();

        yield return new WaitForSeconds(3f);

        _useItemButton.ItemActionCompleted = false;
        _useItemButton.button.interactable = true;

        if (_player.curHP <= 0)
        {
            Debug.Log("Player defeated. Game Over.");
            _uiManager.OpenGameOverScreen();
            yield break; // Stop further actions
        }
        StartCoroutine(PlayerTurn());
    }
}

