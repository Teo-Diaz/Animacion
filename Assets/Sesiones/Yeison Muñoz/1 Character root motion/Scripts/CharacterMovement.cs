using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private Camera camera;
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;
    [SerializeField] private float angularSpeed;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private float rotationThreshold;

    public Character ParentCharacter { get; set; }

    private int speedXHash;
    private int speedYHash;
    private Quaternion targetRotation;

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

        Vector2 input = new Vector2(speedX.CurrentValue, speedY.CurrentValue);
        Vector3 movementDirection = GetMovementDirectionRelativeToCamera(input);

        // Estos valores se usan como parámetros del Animator para blending
        animator.SetFloat(speedXHash, movementDirection.x);
        animator.SetFloat(speedYHash, movementDirection.z);

        SolveCharacterRotation(movementDirection);

        if (!ParentCharacter.IsAiming)
            ApplyCharacterRotation();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();

        speedX.TargetValue = inputValue.x;
        speedY.TargetValue = inputValue.y;
    }

    private void SolveCharacterRotation(Vector3 movementDirection)
    {
        if (movementDirection.sqrMagnitude < 0.01f) return;

        Vector3 flatDir = Vector3.ProjectOnPlane(movementDirection, Vector3.up).normalized;
        targetRotation = Quaternion.LookRotation(flatDir, Vector3.up);
    }

    private void ApplyCharacterRotation()
    {
        float motionMagnitude = Mathf.Sqrt(speedX.TargetValue * speedX.TargetValue + speedY.TargetValue * speedY.TargetValue);
        float rotationSpeed = Mathf.SmoothStep(0, .1f, motionMagnitude);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
    }

    private void ApplyCharacterRotationFromAim()
    {
        Vector3 aimForward = Vector3.ProjectOnPlane(aimTarget.forward, transform.up).normalized;
        Vector3 characterForward = transform.forward;
        float angleCos = Vector3.Dot(characterForward, aimForward);

        float rotationSpeed = Mathf.SmoothStep(0f, 1f, Mathf.Acos(angleCos) * Mathf.Rad2Deg / rotationThreshold);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
    }

    private Vector3 GetMovementDirectionRelativeToCamera(Vector2 input)
    {
        Vector3 camForward = camera.transform.forward;
        Vector3 camRight = camera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        return camForward * input.y + camRight * input.x;
    }
}
