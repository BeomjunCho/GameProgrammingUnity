using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGlobal : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
