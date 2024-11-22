using UnityEngine;

public class BossMonster : Monster
{
    private Player _player;
    public override int max_hp { get; set; }
    public override int cur_hp { get; set; }
    public override int damage { get; set; }

    public void SetUp()
    {
        _player = Object.FindAnyObjectByType<Player>();
        max_hp = 30;
        cur_hp = 30;
        damage = 3;
    }

    public override string Attack()
    {
        string attackResult = "";

        if (_player.shield == 0) // if the player doesn't have a shield, deal damage to HP
        {
            _player.curHP -= damage;
            attackResult = $"Monster attacked you. You lost {damage} HP points.";
        }
        else if (_player.shield > 0) // if the player has a shield, deal damage to the shield
        {
            _player.shield -= damage;
            attackResult = $"Monster attacked you but it was blocked by your shield! Shield took {damage} damage.";

            if (_player.shield < 0) // when the shield goes below 0
            {
                _player.curHP += _player.shield; // carry over the remaining negative value to HP
                attackResult += "\nShield spell is broken!";
                _player.shield = 0; // reset shield to 0
            }
        }

        return attackResult; // Return the result to be used elsewhere
    }

    public override void TakeDamage()
    {

    }

    private void Update()
    {
        if (cur_hp <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
        
