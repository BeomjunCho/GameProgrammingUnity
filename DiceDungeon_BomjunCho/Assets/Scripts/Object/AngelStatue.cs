using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AngelStatue : MonoBehaviour
{
    [SerializeField] private GameObject _portion;
    private bool _isPrayed = false; // Boolean for checking if player already pray
    private int _portionAmount = 2;
    public void HealPlayer()
    {
        if (_isPrayed) return; // Prevent re-Praying
        // spawn portions for corresponding amount
        for (int i = 0; i < _portionAmount; i++) 
        {
            GameObject portion = Instantiate(_portion, transform.position + new Vector3(-8, 15, 0), Quaternion.identity); 
        }
        _isPrayed = true;
    }
}
