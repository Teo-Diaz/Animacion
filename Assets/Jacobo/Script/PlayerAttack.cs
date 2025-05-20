using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private int currentWeapon = 0;
    [SerializeField] private GameObject Sword;
    public Material swordMaterial;
    public float dissolveDuration = 1.0f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", true);
        anim.SetInteger("WeaponType", currentWeapon);
        UpdateWeaponVisibility();
    }

    public void LightAttack(InputAction.CallbackContext ctx)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }

        if (!anim.GetBool("canAttack")) return;
        bool val = ctx.performed;
        if (val)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("canAttack", false);
        }
    }

    public void OnAttackEnding()
    {
        anim.SetBool("canAttack", true);
        anim.SetBool("HeavyAttack", false);
    }

    public void HeavyAttack(InputAction.CallbackContext ctx)
    {
        if (!anim.GetBool("canAttack")) return;
        bool val = ctx.performed;
        if (val)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("canAttack", false);
            anim.SetBool("HeavyAttack", true);
        }
    }

    public void ChangeWeapon(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (currentWeapon == 0) // Cambiar de puño a espada
            {
                anim.SetTrigger("ChangeWeapon");
                currentWeapon = 1;
            }
            else // Cambiar de espada a puños
            {
                anim.SetTrigger("ChangeWeapon");
                currentWeapon = 0;
            }
            anim.SetInteger("WeaponType", currentWeapon);
        }
    }
    
    public void OnWeaponChangeComplete()
    {
        UpdateWeaponVisibility();
    }

    private void UpdateWeaponVisibility()
    {
        if (currentWeapon == 1) // Espada equipada
        {
            Sword.SetActive(true);
        }
        else // Espada guardada
        {
            Sword.SetActive(false);
        }
    }

}
