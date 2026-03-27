using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAt : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction lookAction;

    public Transform playerBody;
    public Transform cameraTarget;

    public float mouseSensitivity = 100f;
    private float pitch = 0f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -40f, 70f);

        cameraTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
