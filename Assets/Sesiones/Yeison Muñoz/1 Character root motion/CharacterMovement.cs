using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;

    private int speedXHash;
    private int speedYHash;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedXHash = Animator.StringToHash("SpeedX");
        speedYHash = Animator.StringToHash("SpeedY");
    }

    private void Update()
    {
        speedX.Update();
        speedY.Update();

        animator.SetFloat(speedXHash, speedX.CurrentValue);
        animator.SetFloat(speedYHash, speedY.CurrentValue);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();

        speedX.TargetValue = inputValue.x;
        speedY.TargetValue = inputValue.y;

        //animator.SetFloat("SpeedX", inputValue.x);
        //animator.SetFloat("SpeedY", inputValue.y);

        Vector3 floorNormal = transform.up;
        Vector3 cameraRealForward = camera.transform.forward;
        float angleInterpolator = Mathf.Abs(Vector3.Dot(cameraRealForward, floorNormal));

        Vector3 cameraForward = Vector3.Lerp(cameraRealForward, camera.transform.up, angleInterpolator).normalized;
        
        Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward, floorNormal).normalized;

        Debug.DrawLine(transform.position, transform.position + cameraForward * 2, Color.magenta, 5);

    }
}