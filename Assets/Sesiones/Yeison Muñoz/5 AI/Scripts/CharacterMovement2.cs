using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CharacterMovement2 : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private FloatDampener speedX;
    [SerializeField] private FloatDampener speedY;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMotionVector(float targetX, float targetY)
    {
        speedX.TargetValue = targetX;
        speedY.TargetValue = targetY;
    }

    private void ApplyMotion()
    {
        speedX.Update();
        speedY.Update();

        animator.SetFloat("SpeedX", speedX.CurrentValue);
        animator.SetFloat("SpeedY", speedY.CurrentValue);
    }

    public void Update()
    {
        if (animator.updateMode != AnimatorUpdateMode.Normal)
            return;
        ApplyMotion();
    }

    private void FixedUpdate()
    {
        if (animator.updateMode != AnimatorUpdateMode.Fixed)
            return;
        ApplyMotion();
    }
}
