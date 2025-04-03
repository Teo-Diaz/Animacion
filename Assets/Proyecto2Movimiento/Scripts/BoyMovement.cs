using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class BoyMovement : MonoBehaviour //ICharacterComponent
{
    [SerializeField] private Camera camera;
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;
    [SerializeField] private float angularSpeed;
    [SerializeField] private float rotationThreshold;

    //public Character ParentCharacter { get; set; }

    private int speedXHash;
    private int speedYHash;
    Quaternion targetRotation;

    private Animator animator;

    private void SolveCharacterRotation()
    {
        Vector3 floorNormal = transform.up;
        Vector3 cameraRealForward = camera.transform.forward;
        float angleInterpolator = Mathf.Abs(Vector3.Dot(cameraRealForward, floorNormal));
        Vector3 cameraForward = Vector3.Lerp(cameraRealForward, camera.transform.up, angleInterpolator).normalized;
        Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward, floorNormal).normalized;
        Debug.DrawLine(transform.position, transform.position + cameraForward * 2, Color.magenta, 5);
        targetRotation = Quaternion.LookRotation(characterForward, floorNormal);
    }

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

        SolveCharacterRotation();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();

        speedX.TargetValue = inputValue.x;
        speedY.TargetValue = inputValue.y;
    }

    private void ApplyCharacterRotation()
    {
        float motionMagnitude = Mathf.Sqrt(speedX.TargetValue * speedX.TargetValue + speedY.TargetValue * speedY.TargetValue);
        float rotationSpeed = Mathf.SmoothStep(0, .1f, motionMagnitude);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
    }
}