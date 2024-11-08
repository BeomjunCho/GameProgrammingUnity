
using UnityEngine;

public class ScrollBook : MonoBehaviour
{
    [SerializeField] private GameObject[] _scrolls;
    [SerializeField] private float _throwForce;
    public void ScrollSummon()
    {
        Destroy(gameObject); // Destory this game object

        // Spawn random scroll from array and throw
        if (_scrolls != null)
        {
            GameObject scroll = Instantiate(_scrolls[Random.Range(0, _scrolls.Length)], transform.position, Quaternion.identity);

            Rigidbody rb = scroll.GetComponent<Rigidbody>();

            rb.AddForce(transform.up * _throwForce + transform.right * (_throwForce / 2) * -1, ForceMode.Impulse);
        }
    }
}
