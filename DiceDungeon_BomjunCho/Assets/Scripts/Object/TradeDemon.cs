
using UnityEngine;

public class TradeDemon : MonoBehaviour
{
    [SerializeField] private GameObject _dragonSword; // item to throw
    [SerializeField] private GameObject _spawnPosition; // item spawn position
    [SerializeField] private GameObject _Deco; // Decoration dragon sword
    [SerializeField] private float _throwForce; // How much power for throwing
    private bool _isTrade = false;

    public void TradeWithDemon()
    {
        if (_isTrade) return; // Prevent re-trading
        Destroy(_Deco); // Destroy decoration for imitating trade
        
        // Spawn and Throw item
        GameObject thrownItem = Instantiate(_dragonSword, _spawnPosition.transform.position, Quaternion.identity);

        Rigidbody rb = thrownItem.GetComponent<Rigidbody>();

        rb.AddForce(transform.up * _throwForce + transform.right * (_throwForce / 2) * -1, ForceMode.Impulse);

        _isTrade = true;
    }

}
