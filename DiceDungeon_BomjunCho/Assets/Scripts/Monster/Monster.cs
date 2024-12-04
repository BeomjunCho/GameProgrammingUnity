using UnityEngine;

/// <summary>
/// The Monster class serves as an abstract base for all monster types in the game.
/// It defines properties and methods that every derived monster must implement, 
/// such as health, damage, and attack behavior.
/// </summary>
public abstract class Monster : MonoBehaviour
{
    public abstract int max_hp { get; set; } // monster max hp
    [SerializeField] public abstract int cur_hp { get; set; } // monster current hp

    public abstract int damage { get; set; } // monster damage
    public abstract string Attack(); // attack 
}
        
