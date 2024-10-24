using UnityEngine;

public abstract class Item : MonoBehaviour
{

    public int ID { get; set; } // Item id
    protected int randomNumber(int maxIndex) // random number generator
    {
        int RanNum = Random.Range(1, maxIndex + 1);
        return RanNum;
    }
}


