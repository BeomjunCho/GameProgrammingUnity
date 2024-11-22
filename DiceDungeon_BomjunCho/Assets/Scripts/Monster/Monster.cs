using UnityEngine;
public abstract class Monster : MonoBehaviour
{
    public abstract int max_hp { get; set; } // monster max hp
    public abstract int cur_hp { get; set; } // monster current hp
    public abstract int damage { get; set; } // monster damage
    public abstract string Attack(); // attack 
    public abstract void TakeDamage(); // take damage
}
        
