using UnityEngine;

/// <summary>
/// This class prevents destorying managers
/// </summary>
public class GameGlobal : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
