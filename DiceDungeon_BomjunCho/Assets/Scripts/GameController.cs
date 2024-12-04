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
    public Inventory InventoryPrefab { get => _inventoryPrefab; set => _inventoryPrefab = value; }


    public void Start()
    {
        Debug.Log("GameManager Start");
        // zero our manager position
        transform.position = Vector3.zero;

        // create an instance of the map manager
        InstantiatePrefabs();
        // create the map
        SetUpInstances();
        // Start game
        StartGame();
    }

    private void StartGame()
    {
        // Intro
        Debug.Log("Hello, World!");

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
        // Create rooms
        _gameMap.CreateMap(ref _inventory);
        // Set up Player
        _playerController.SetUp(ref _inventory);
        // Get the Interaction component from the instantiated PlayerController instance
        _interaction = _playerController.GetComponent<Interaction>();

        if (_interaction != null)
        {
            _interaction.SetUp(ref _inventory);
        }
        else
        {
            Debug.LogWarning("Interaction component not found on PlayerControllerPrefab instance.");
        }
        
        // Set up Inventory
        _inventory.SetUp();
    }
}




