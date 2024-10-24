using UnityEngine;


public class Player : MonoBehaviour
{
    public string userName = "";
    public int hp = 10; // health points
    public int shield = 0; // shield points if it is over 0 then, it takes damage from hp

    // user inventory. It is only one inventory in the game
    public Inventory inventory = new Inventory();

    public void ShowHp() // Show user hp
    {
        Debug.Log($"\nUser hp: {hp}");
        Debug.Log($"\nShield amount: {shield}");
    }

    public void ShowPlayerState() // Show user hp, shield amount and inventory
    {
        inventory.ShowInventory();                       // show inventory
        Debug.Log($"Your Health point: {hp}"); // hp
        Debug.Log($"Shield: {shield}");        //shield amount
    }

}

