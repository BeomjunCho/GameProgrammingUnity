using UnityEngine;

public class BossMonster : Monster
{
    public override string ToString()
    {
        return "Pumpking"; //representation
    }
    public BossMonster()
    {
        hp = 30;
        damage = 3;
    }
    public override int hp { get; set; }
    public override int damage { get; set; }

    public override void Attack(Player user)
    {
        if (user.shield == 0)// if user doesn't have shield, give damage to hp
        {
            user.hp -= damage;
            Debug.Log("Pumpking attacked you. You lost 3 hp points");
        }
        else if (user.shield > 0) // if user has shield, give damage to shield
        {
            user.shield -= damage;
            Debug.Log("Pumpking attacked you but it is blocked by your shield!\nShield -3");
            if (user.shield < 0)// when shield less than 0
            {
                Debug.Log("Shield spell is broken!");
                user.shield = 0;// set shield 0 again
            }
        }
    }

    public override void ShowHp()
    {
        Debug.Log($"\nBoss Moster hp: {hp}");
    }

}
        
