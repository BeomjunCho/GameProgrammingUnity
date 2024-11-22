using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _walkSpeed; // Walk speed
    [SerializeField] private float _runSpeed; // Run speed
    [SerializeField] private float _jumpPower; // Jump power
    [SerializeField] private float _lookSpeed; // how fast player camera moves
    [SerializeField] private float _lookXLimit; // Up Down angle limit


    Vector3 moveDirection = Vector3.zero; // Direction for moving
    float rotationX = 0;

    public bool canMove; // Player only moves in true statement
    
    Rigidbody rb; 

    public void SetUp(ref Inventory inventory)
    {
        rb = GetComponent<Rigidbody>(); // Get player rigid body
        rb.freezeRotation = true;  // To avoid unwanted rotation
        canMove = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (canMove)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            bool isRunning = Input.GetKey(KeyCode.LeftShift); // Running when left shift is pressed
            float speed = isRunning ? _runSpeed : _walkSpeed; // Change speed depending on running state

            float moveForward = Input.GetAxis("Vertical") * speed; // Calculate forward/backward direction float
            float moveSide = Input.GetAxis("Horizontal") * speed; // Calculate right/left direction float

            // Update moveDirection with input directions
            moveDirection = forward * moveForward + right * moveSide;

            // Apply horizontal movement
            Vector3 velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
            rb.velocity = velocity;

            // Apply rotation
            rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -_lookXLimit, _lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);

            // Jump handling
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            }
        }

    }
    

    // return true or false depending on player and ground distance
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
