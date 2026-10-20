using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5;
    public float lookSensitivity = 2;
    public Transform playerCamera;

    private Rigidbody rb;
    private float xRotation = 0;


    void Start()
    {
        // Get the Rigidbody component and freeze rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Look
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation -= mouseY;
        // Stop the camera from rotating too far up or down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        // Rotate the camera
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Rotate the player object
        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        // Movement 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate the movement direction based on where the player faces
        Vector3 move = transform.right * x + transform.forward * z;

        // Normalise the direction so diagonal movement isnt faster
        move = move.normalized * walkSpeed;

        // Apply the movement while keeping the players vertical velocity the same
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}
