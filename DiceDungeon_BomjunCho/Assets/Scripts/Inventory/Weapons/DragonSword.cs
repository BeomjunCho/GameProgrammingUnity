using UnityEngine;

public class DragonSword : Weapon
{
    [SerializeField] private Monster _monster;

    private static readonly int _id = 3;  // Unique ID for the DragonSword class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is dragon sword. This weapon can cause 1-20 random damage to monster.";
    public override string description => _description;

    public override int maxDamage { get; set; } = 20;

    protected override string _dynamicText { get; set; }

    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    public override void Attack() // attack monster for random damage 
    {
        int result = randomNumber(maxDamage);
        _monster.cur_hp -= result;
        _dynamicText = $"You attacked the monster with Dragon Sword for {result} damage.";
    }

    public override string ItemActionText()
    {
        return _dynamicText;
    }
}


