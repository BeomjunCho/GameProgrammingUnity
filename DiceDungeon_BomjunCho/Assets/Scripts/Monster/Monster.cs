using UnityEngine;
public abstract class Monster : MonoBehaviour
{
    public abstract int hp { get; set; } // monster hp
    public abstract int damage { get; set; } // monster damage
    public abstract void Attack(Player user); //attack method
    public abstract void ShowHp(); // show monster hp method
}
        
