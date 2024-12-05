using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Transform _chestLid; // The lid of chest to rotate(open)
    [SerializeField] private GameObject[] _itemToThrow; // Array for items to throw
    [SerializeField] private float _throwForce; // How much power for throwing

    private bool _isOpen = false; // Boolean for checking if chest is opened

    /// <summary>
    /// Method to open the chest and initiate item throwing.
    /// </summary>
    public void OpenChest()
    {
        if (_isOpen) return; // Prevent re-opening
        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.ChestOpen], 1.0f);

        // Rotate the chest lid by 90 degrees on the z-axis
        _chestLid.localRotation = Quaternion.Euler(_chestLid.localRotation.x, _chestLid.localRotation.y, -90);
        _isOpen = true; // chest is opened

        // Start coroutine to delay the item instantiation
        StartCoroutine(ThrowItemAfterDelay());        
    }

    /// <summary>
    /// Coroutine to wait for a short delay before throwing an item from the chest.
    /// </summary>
    /// <returns>IEnumerator for Unity's coroutine system.</returns>
    private IEnumerator ThrowItemAfterDelay()
    {
        // 0.5 sec wait
        yield return new WaitForSeconds(0.5f);

        // Instantiate random item from array and throw it
        if (_itemToThrow != null)
        {
            GameObject thrownItem = Instantiate(_itemToThrow[Random.Range(0, _itemToThrow.Length)], _chestLid.position + new Vector3(0, 3, 0), Quaternion.identity);

            Rigidbody rb = thrownItem.GetComponent<Rigidbody>();

            //Throw item to right direction
            rb.AddForce(transform.up * _throwForce + transform.right * (_throwForce / 2) * -1, ForceMode.Impulse);
        }
    }
}
