/// <summary>
/// Weapon items will inherits from this class
/// It is used in inventory to check if item is weapon
/// </summary>

public abstract class Weapon : Item
{
    // Create an instance of DieRoller in the Weapon class
    public abstract void Attack();
    public abstract int maxDamage { get; set; }
}


