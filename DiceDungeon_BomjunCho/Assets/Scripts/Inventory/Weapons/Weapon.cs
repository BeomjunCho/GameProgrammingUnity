﻿using UnityEngine;

public abstract class Weapon : Item
{
    // Create an instance of DieRoller in the Weapon class
    public abstract void Attack(Monster monster);
    public abstract int maxDamage { get; set; }
}

