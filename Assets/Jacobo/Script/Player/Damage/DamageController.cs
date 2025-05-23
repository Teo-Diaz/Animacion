using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class DamageController : MonoBehaviour, IDamageReceiver
{
    private Animator anim;
    [SerializeField] private int faction;
    public PanelController panelController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ReceiveDamage(IDamageSender perpetrator, DamagePayload payload)
    {
        bool isAlive = GetComponent<PlayerHealthManager>().UpdateHealth(-payload.damageAmount);
        Vector3 localDamageDir = transform.InverseTransformDirection(payload.position - transform.position).normalized;

        if (isAlive)
        {
            anim.ResetTrigger("Damaged");

            if (Mathf.Abs(localDamageDir.x) >= Mathf.Abs(localDamageDir.z))
            {
                anim.SetFloat("DamageX", localDamageDir.x);
                anim.SetFloat("DamageY", 0f);
            }
            else
            {
                anim.SetFloat("DamageX", 0f);
                anim.SetFloat("DamageY", localDamageDir.z);
            }

            anim.SetInteger("DamageLevel", (int)payload.severity);

            anim.SetTrigger("Damaged");

            if (TryGetComponent<PlayerInput>(out PlayerInput input))
            {
                anim.SetBool("canAttack", true);
            }

            if (TryGetComponent<PlayerMovement>(out PlayerMovement movement))
            {
                movement.ResetInput();
            }

        }
        else
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        anim.SetTrigger("Die");
        anim.SetBool("Dead", true); 

        if (TryGetComponent<PlayerInput>(out PlayerInput input))
        {
            input.enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void TriggerDeathPanel()
    {
        panelController.ShowDeathPanel();
    }

    public int Faction => faction;

}
