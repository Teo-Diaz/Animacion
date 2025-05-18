using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform target; // Objeto a rotar (ej. CameraRig o camara)
    [SerializeField] private float horizontalRotationSpeed = 150f;
    [SerializeField] private float verticalRotationSpeed = 150f;
    [SerializeField] private Vector2 verticalRotationLimits = new Vector2(-60f, 60f);

    private float horizontalRotation;
    private float verticalRotation;
    private Vector2 inputValue;

    public void OnLook(InputAction.CallbackContext context)
    {
        inputValue = context.ReadValue<Vector2>();
    }

    private void ApplyLookRotation()
    {
        if (target == null)
        {
            Debug.LogError("Target no asignado en BoyLook.");
            return;
        }

        // Aplica rotaci�n horizontal y vertical
        horizontalRotation += inputValue.x * horizontalRotationSpeed * Time.deltaTime;
        verticalRotation -= inputValue.y * verticalRotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, verticalRotationLimits.x, verticalRotationLimits.y);

        target.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    private void Update()
    {
        ApplyLookRotation();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
