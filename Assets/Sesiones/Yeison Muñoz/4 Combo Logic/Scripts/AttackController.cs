using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(CharacterState))]
public class AttackController : MonoBehaviour
{

    [SerializeField] private float lightAttackCost;
    [SerializeField] private float heavyAttackCost;

    private Animator anim;
    private CharacterState characterState;

    public FloatUEvent OnAttack;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        characterState = GetComponent<CharacterState>();
    }

    //private bool CanAttack()
    //{
    //    return anim.GetFloat("ControlWeight") > 0;
    //}

    public void OnLightAttack(CallbackContext ctx)
    {
        if (ctx.performed)
        {

            //anim.SetTrigger("LightAttack");
            //bool attackState = anim.GetFloat("ControlWeight") > 0;
            //if(CanAttack())
            //{
            //    //OnAttack?.Invoke(lightAttackCost);
            //}
        }
    }

    public void OnHeavyAttack(CallbackContext ctx)
    {
        if (ctx.performed || ctx.canceled)
        {
            //anim.SetTrigger("HeavyAttack");
            ////OnAttack?.Invoke(heavyAttackCost);
        }
    }
}
