using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // It will creates map
    [SerializeField] private Map _gameMapPrefab;
    // It will creates PlayerController
    [SerializeField] private PlayerController _playerControllerPrefab;
    // It will creates Inventory
    [SerializeField] private Inventory _inventoryPrefab;

    private Map _gameMap;
    private PlayerController _playerController;
    private Inventory _inventory;
    private Interaction _interaction;
    private Player _player;
    public Inventory InventoryPrefab { get => _inventoryPrefab; set => _inventoryPrefab = value; }

    public void StartGame()
    {
        Debug.Log("GameManager Start");
        // zero our manager position
        transform.position = Vector3.zero;

        // create an instance of the map manager
        InstantiatePrefabs();
        // create the map
        SetUpInstances();
    }

    void InstantiatePrefabs()
    {
        // Instantiate Game map
        _gameMap = Instantiate(_gameMapPrefab, transform);
        _gameMap.transform.position = Vector3.zero;
        // Instantiate Player controller
        _playerController = Instantiate(_playerControllerPrefab, transform);
        _playerController.transform.position = new Vector3(-14, -15, 2);
        // Instantiate Inventory       
        _inventory = Instantiate(InventoryPrefab, transform);
        _inventory.transform.position = Vector3.zero;

    }

    void SetUpInstances()
    {
        _gameMap.SetUp();
        // Create rooms
        _gameMap.CreateMap(ref _inventory);
        // Set up Player
        _playerController.SetUp(ref _inventory);
        // Get the Interaction component from the instantiated PlayerController instance
        _interaction = _playerController.GetComponent<Interaction>();
        _player = _playerController.GetComponent<Player>();
        if (_interaction != null && _player != null)
        {
            _interaction.SetUp(ref _inventory);
            _player.Initialize();
        }
        else
        {
            Debug.LogWarning("Interaction or player component not found on PlayerControllerPrefab instance.");
        }
        
        // Set up Inventory
        _inventory.SetUp();
    }
}




