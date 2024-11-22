using UnityEngine;

public class Dagger : Weapon
{
    [SerializeField] private Monster _monster;

    private static readonly int _id = 1;  // Unique ID for the Dagger class
    public override int ID => _id;        // Implements the ID property in Item

    private static readonly string _description = "This is dagger. This weapon can cause 1-8 random damage to monster.";
    public override string description => _description;

    public override int maxDamage { get; set; } = 8;

    protected override string _dynamicText { get; set; }

    public void GetMonster(Monster monster)
    {
        _monster = monster;
    }

    public override void Attack() // attack monster for random damage 
    {
        int result = randomNumber(maxDamage);
        _monster.cur_hp -= result;
        _dynamicText = $"You attacked the monster with Dagger for {result} damage.";    
            
    }

    public override string ItemActionText()
    {
        return _dynamicText;
    }
}


