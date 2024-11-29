using UnityEngine;

/// <summary>
/// The BossMonster class represents a boss monster with high health and damage.
/// It includes behavior for attacking the player, taking damage, and dropping loot upon death.
/// </summary>
public class BossMonster : Monster
{
    [SerializeField] private GameObject _HammerPrefab;

    private Player _player;
    public override int max_hp { get; set; }
    public override int cur_hp { get; set; }
    public override int damage { get; set; }

    /// <summary>
    /// Initializes the boss monster's stats and finds the Player in the scene.
    /// </summary>
    public void SetUp()
    {
        _player = Object.FindAnyObjectByType<Player>();
        max_hp = 30;
        cur_hp = 30;
        damage = 9;
    }

    /// <summary>
    /// Simulates the boss monster's attack on the player. The damage dealt depends on the player's shield status.
    /// Returns a string describing the attack's outcome.
    /// </summary>
    /// <returns>A string summarizing the result of the attack.</returns>
    public override string Attack()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.MonsterAttack], 3.0f);
        string attackResult = "";
        damage = Random.Range(0, damage);

        if (damage == 0)
        {
            attackResult = "The boss monster attacked, but you dodged!";
        }
        else
        {
            if (_player.shield == 0) // if the player doesn't have a shield, deal damage to HP
            {
                _player.curHP -= damage;
                attackResult = $"Dice number was {damage}!\nMonster attacked you. You lost {damage} HP points.";
            }
            else if (_player.shield > 0) // if the player has a shield, deal damage to the shield
            {
                int shieldBeforeDamage = _player.shield; // Store shield amount before damage
                _player.shield -= damage;

                if (_player.shield < 0) // when the shield goes below 0
                {
                    _player.curHP += _player.shield; // carry over the remaining negative value to HP
                    attackResult = $"Dice number was {damage}!\nThe shield absorbed part of the attack, taking {shieldBeforeDamage} damage, but some still got through." +
                        $"\nShield spell is broken!";
                    _player.shield = 0; // Reset shield to 0
                }
                else // When the shield absorbs all the damage
                {
                    attackResult = $"Dice number was {damage}!\nMonster attacked you but it was blocked by your shield! Shield took {damage} damage.";
                }
            }
        }
        
        return attackResult; // Return the result to be used elsewhere
    }

    /// <summary>
    /// Handles the death of the boss monster.
    /// Deactivates the boss monster and spawns a hammer as a reward.
    /// </summary>
    public void BossMonsterDead()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.MonsterDead], 3.0f);
        Instantiate(_HammerPrefab, this.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
        
