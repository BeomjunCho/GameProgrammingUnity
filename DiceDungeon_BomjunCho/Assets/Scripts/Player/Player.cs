using UnityEngine;

public class Player : MonoBehaviour 
{
    public int maxHp = 10; // max health points
    private int _curHP = 8; // current health points
    private int _shield = 0; // shield points if it is over 0 then, it takes damage from hp
    public int curHP { get => _curHP; set => _curHP = value; }
    public int shield { get => _shield; set => _shield = value; }

    public void Initialize()
    {
        maxHp = 10;
        _curHP = 4;
        _shield = 0;
        Debug.Log("Initialize() executed. Current HP set to: " + _curHP);
    }
}

