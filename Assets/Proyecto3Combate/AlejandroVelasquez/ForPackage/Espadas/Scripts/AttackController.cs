using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

[RequireComponent(typeof(Animator))]
public class AttackController : MonoBehaviour
{

    [SerializeField] private float lightAttackCost;
    [SerializeField] private float heavyAttackCost;

    private AttackHitboxController hitboxController;

    private Animator anim;

    public bool _lightAttack { get; set; }
    public bool _heavyAttack { get;  set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hitboxController = GetComponent<AttackHitboxController>();
    }

    public void OnLightAttack(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (Game.Instance.PlayerOne.CurrentStamina > 0)
                anim.SetTrigger("LightAttack");
            
        }
        _lightAttack = true;
        _heavyAttack = false;
    }

    public void OnHeavyAttack(CallbackContext ctx)
    {
        if (ctx.performed || ctx.canceled)
        {
            if (Game.Instance.PlayerOne.CurrentStamina > 0)
                anim.SetTrigger("HeavyAttack");
            
        }
        _lightAttack = false;
        _heavyAttack = true;
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

    public void ToggleAttackHitbox(int hitboxId)
    {
        hitboxController.ToggleHitboxes(hitboxId);
    }

    public void CleanupAttackHitbox()
    {
        hitboxController.CleanupHitboxes();
    }
}
