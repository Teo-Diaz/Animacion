using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{

    [SerializeField] private float lightAttackCost;
    [SerializeField] private float heavyAttackCost;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnLightAttack(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (Game.Instance.PlayerOne.CurrentStamina > 0)
                anim.SetTrigger("LightAttack");
        }
    }

    public void OnHeavyAttack(CallbackContext ctx)
    {
        if (ctx.performed || ctx.canceled)
        {
            if (Game.Instance.PlayerOne.CurrentStamina > 0)
                anim.SetTrigger("HeavyAttack");
        }
    }

    public void DepleteStamina(float amount)
    {
        Game.Instance.PlayerOne.DepleteStamina(amount);
    }

    public void DepleteStaminaWithParameter(string parameter)
    {
        float motionValue = GetComponent<Animator>().GetFloat(parameter);
        DepleteStamina(motionValue);
    }
}
