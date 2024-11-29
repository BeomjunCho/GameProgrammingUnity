using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the movement of a stone wall when the player enters its trigger.
/// The wall moves upward a specified distance at a defined speed.
/// </summary>
public class StoneWallMove : MonoBehaviour
{

    [SerializeField] private float _moveUpDistance; // Distance to move up
    [SerializeField] private float _moveSpeed;      // Speed of movement
    private Vector3 _originalPosition; // Original position 
    private bool _isMoving = false;    // Check if the wall is moving

    /// <summary>
    /// Initializes the original position of the stone wall.
    /// </summary>
    private void Start()
    {
        // Save the original position of the object
        _originalPosition = transform.position;
    }

    /// <summary>
    /// Detects when a collider enters the trigger zone and starts moving the wall if it's the player.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger has the "Player" tag and stone wall is not moving
        if (other.CompareTag("Player") && !_isMoving)
        {
            StartCoroutine(MoveUp());
        }
    }

    /// <summary>
    /// Coroutine to move the stone wall upward to the target position over time.
    /// </summary>
    /// <returns>An enumerator for Unity's coroutine system.</returns>
    private IEnumerator MoveUp()
    {
        _isMoving = true;
        Vector3 targetPosition = _originalPosition + Vector3.up * _moveUpDistance;
        float distanceCovered = 0f;

        // This loop continues as long as distanceCovered is less than 1f, meaning the object has not yet reached the target position.
        // Using 1f as the stopping condition makes this loop act like a percentage
        while (distanceCovered < 1f) // Ensures the movement runs until the end
        {
            distanceCovered += _moveSpeed * Time.deltaTime; // Distance = Speed * Time  
            transform.position = Vector3.Lerp(_originalPosition, targetPosition, distanceCovered); // Move stone door
            yield return null; // Pauses the coroutine until the next frame
        }

        // Ensure the position is set to the exact target position at the end
        transform.position = targetPosition;
        _isMoving = false;
    }

}
