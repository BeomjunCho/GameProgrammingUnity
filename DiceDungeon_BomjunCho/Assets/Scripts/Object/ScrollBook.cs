using UnityEngine;

/// <summary>
/// The ScrollBook class represents a book that, when destroyed, spawns a random scroll from a predefined array
/// and throws it with a specified force.
/// </summary>
public class ScrollBook : MonoBehaviour
{
    [SerializeField] private GameObject[] _scrolls; // Array of scroll prefabs to choose from.
    [SerializeField] private float _throwForce;     // The force used to throw the spawned scroll.

    /// <summary>
    /// Summons a random scroll from the predefined array, destroys the book object, 
    /// and throws the scroll with a specified force.
    /// </summary>
    public void ScrollSummon()
    {
        Destroy(gameObject); // Destroy this game object (the book).

        AudioManager.Instance.PlaySfx(AudioManager.Instance.sfxList[(int)SfxTrack.BookSummon], 1.0f);

        // Check if the _scrolls array is not null, then spawn and throw a random scroll.
        if (_scrolls != null)
        {
            // Instantiate a random scroll from the _scrolls array at the book's position.
            GameObject scroll = Instantiate(_scrolls[Random.Range(0, _scrolls.Length)], transform.position, Quaternion.identity);

            // Get the Rigidbody component of the spawned scroll for physics-based movement.
            Rigidbody rb = scroll.GetComponent<Rigidbody>();

            // Apply a force to the scroll to throw it upwards and slightly to the side.
            rb.AddForce(transform.up * _throwForce + transform.right * (_throwForce / 2) * -1, ForceMode.Impulse);
        }
    }
}
