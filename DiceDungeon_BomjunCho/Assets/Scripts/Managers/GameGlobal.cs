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

    /// <summary>
    /// Resets the Game by destroying all its children.
    /// </summary>
    public void ResetGame()
    {
        // Destroy all child objects of the GameController
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Optionally log for debugging
        Debug.Log("game global reset: All children destroyed.");
    }
}
