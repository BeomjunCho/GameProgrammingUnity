using UnityEngine;

public class NormalMonster : Monster
{
    public override string ToString()
    {
        return "Minotaur"; //representation
    }
    public NormalMonster()
    {
        hp = 10;
        damage = 1;
    }
    public override int hp { get; set; }
    public override int damage { get; set; }

    public override void Attack(Player user)
    {
        if (user.shield == 0) // if user doesn't have shield, give damage to hp
        {
            user.hp -= damage;
            Debug.Log("Minotaur attacked you. You lost 1 hp points");
        }
        else if (user.shield > 0)// if user has shield, give damage to shield
        {
            user.shield -= damage;
            Debug.Log("Minotaur attacked you but it is blocked by your shield!\nShield -1");
            if (user.shield < 0) // when shield less than 0
            {
                Debug.Log("Shield spell is broken!");
                user.shield = 0; // set shield 0 again
            }
        }
    }

    public override void ShowHp()
    {
        Debug.Log($"\nMoster hp: {hp}");
    }

}
        
