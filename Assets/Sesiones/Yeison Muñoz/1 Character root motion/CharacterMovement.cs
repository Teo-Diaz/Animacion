using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;

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
        animator.SetFloat("SpeedX", speedX);
        animator.SetFloat("SpeedY", speedY);
    }
}