using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    public void ButtonBackToMain()
    {
        _uiManager.OpenMainMenu();
    }

}
