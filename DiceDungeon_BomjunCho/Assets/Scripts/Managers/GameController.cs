﻿using System.Linq;
using UnityEngine;

/// <summary>
/// Controls the overall game setup, including map creation, player instantiation, 
/// inventory setup, and interaction initialization.
/// </summary>
public class GameController : MonoBehaviour
{
    // Prefab references for instantiating the game components
    [SerializeField] private Map _gameMapPrefab;                // Prefab for the map manager
    [SerializeField] private PlayerController _playerControllerPrefab; // Prefab for the player controller
    [SerializeField] private Inventory _inventoryPrefab;        // Prefab for the inventory system

    // Instance variables for managing the instantiated components
    private Map _gameMap;                  // The instantiated map manager
    private PlayerController _playerController; // The instantiated player controller
    private Interaction _interaction;      // Interaction component from the player controller
    private Player _player;                // Player component from the player controller
    private Inventory _inventory;          // The instantiated inventory system

    // Public accessor for the inventory prefab
    public Inventory InventoryPrefab { get => _inventoryPrefab; set => _inventoryPrefab = value; }

    /// <summary>
    /// Starts the game setup process, including initializing the map, player, and inventory.
    /// </summary>
    public void StartGame()
    {
        Debug.Log("GameManager Start");

        // Reset GameController's position to ensure clean setup
        transform.position = Vector3.zero;

        // Instantiate prefabs and set up game components
        InstantiatePrefabs();
        SetUpInstances();
    }

    /// <summary>
    /// Instantiates the map, inventory, and player controller prefabs.
    /// </summary>
    void InstantiatePrefabs()
    {
        // Instantiate the game map and position it at the origin
        _gameMap = Instantiate(_gameMapPrefab, transform);
        _gameMap.transform.position = Vector3.zero;

        // Instantiate the inventory and position it at the origin
        _inventory = Instantiate(InventoryPrefab, transform);
        _inventory.transform.position = Vector3.zero;

        // Instantiate the player controller and position it at a specific starting location
        _playerController = Instantiate(_playerControllerPrefab, transform);
        _playerController.transform.position = new Vector3(-14, -15, 2);
    }

    /// <summary>
    /// Sets up the map, inventory, player controller, and interaction system.
    /// </summary>
    void SetUpInstances()
    {
        // Retrieve the Interaction and Player components from the instantiated PlayerController
        _interaction = _playerController.GetComponent<Interaction>();
        _player = _playerController.GetComponent<Player>();

        // Set up the map and create its rooms
        _gameMap.SetUp();
        _gameMap.CreateMap(ref _inventory);

        // Set up the player controller
        _playerController.SetUp();

        // Configure interaction and player systems if the components are available
        if (_interaction != null && _player != null)
        {
            _interaction.SetUp(ref _inventory); // Pass the inventory reference to the interaction system
            _player.Initialize();              // Initialize the player settings
        }
        else
        {
            Debug.LogWarning("Interaction or player component not found on PlayerControllerPrefab instance.");
        }

        // Set up the inventory system
        _inventory.SetUp();
    }
}
