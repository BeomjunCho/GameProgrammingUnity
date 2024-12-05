using UnityEngine;

/// <summary>
/// The AngelStatue class represents a statue that grants healing potions to the player when prayed to.
/// Once activated, the statue cannot be prayed to again.
/// </summary>
public class AngelStatue : MonoBehaviour
{
    [SerializeField] private GameObject _portion;
    
    private bool _isPrayed = false; // Boolean for checking if player already pray
    private int _portionAmount = 2;

    /// <summary>
    /// Grants healing potions to the player if the statue has not already been prayed to.
    /// Spawns a predefined number of potion objects near the statue's position.
    /// </summary>
    public void HealPlayer()
    {
        if (_isPrayed) return; // Prevent re-Praying
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.Pray], 1.0f);
        // spawn portions for corresponding amount
        for (int i = 0; i < _portionAmount; i++) 
        {
            GameObject portion = Instantiate(_portion, transform.position + new Vector3(-8, 15, 0), Quaternion.identity); 
        }
        _isPrayed = true;
    }
}
