using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract int ID { get; } // Item ID
    public Sprite Icon; // Sprite for using in Inventory Display
    protected int randomNumber(int maxIndex) // random number generator
    {
        int RanNum = Random.Range(1, maxIndex + 1);
        return RanNum;
    }
}


