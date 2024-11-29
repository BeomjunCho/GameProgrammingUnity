using UnityEngine;

/// <summary>
/// The Player class represents the player's character, managing their health, level, shield, 
/// and other core attributes. It includes methods for initialization and leveling up.
/// </summary>
public class Player : MonoBehaviour 
{
    public int maxHp = 10; // max health points
    private int _curHP = 8; // current health points
    private int _level = 1;
    private int _shield = 0; // shield points if it is over 0 then, it takes damage from hp
    public int level { get => _level; set => _level = value; }
    public int curHP
    {
        get => _curHP;
        set => _curHP = Mathf.Clamp(value, 0, maxHp); // Clamp curHP between 0 and maxHp
    }
    public int shield { get => _shield; set => _shield = value; }

    /// <summary>
    /// Initializes the player's stats to their starting values.
    /// Resets health, shield, and level to their default states.
    /// </summary>
    public void Initialize()
    {
        maxHp = 10;
        _curHP = 10;
        _shield = 0;
        _level = 1;
        Debug.Log("Initialize() executed. Current HP set to: " + _curHP);
    }

    /// <summary>
    /// Levels up the player, increasing their max health, current health, and level.
    /// Provides an additional health bonus for levels 1–10.
    /// </summary>
    public void LevelUp()
    {
        if (_level <= 10)
        {
            level++;
            maxHp++;
            curHP++;
            curHP++;
        }
        else
        {
            level++;
            maxHp++;
            curHP++;
        }
    }

}

