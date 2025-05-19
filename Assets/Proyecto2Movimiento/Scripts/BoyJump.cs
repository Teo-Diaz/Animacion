using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class BoyJump : MonoBehaviour, ICharacterComponent
{
    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundedCooldown = 0.2f;

    private float verticalVelocity;
    private bool isGrounded;
    private float lastJumpTime;

    private CharacterController controller;
    private Animator animator;

    private int isGroundedHash;
    private int verticalSpeedHash;

    public Character ParentCharacter { get; set; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isGroundedHash = Animator.StringToHash("IsGrounded");
        verticalSpeedHash = Animator.StringToHash("VerticalSpeed");
    }

    private void Update()
    {
        CheckGround();
        ApplyGravity();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            lastJumpTime = Time.time;
        }
    }

    private void CheckGround()
    {
        bool recentlyJumped = Time.time - lastJumpTime < groundedCooldown;

        if (recentlyJumped)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // pequeño "pegue" al suelo
        }

        animator.SetBool(isGroundedHash, isGrounded);
    }

    private void ApplyGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 gravityMove = Vector3.up * verticalVelocity;
        controller.Move(gravityMove * Time.deltaTime);

        animator.SetFloat(verticalSpeedHash, verticalVelocity);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
