using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject Sword;

    [Header("Combos")]
    [SerializeField] private int maxCombo = 3;
    [SerializeField] private float comboResetTime = 0.8f;
    private enum WeaponType { Punch = 0, Sword = 1 }
    private Animator anim;
    private WeaponType currentWeapon = WeaponType.Punch;
    private int   comboIndex;
    private float lastAttackTime;
    private bool  comboWindowOpen;

    // ─────────  Hashes de parámetros del Animator  ─────────
    private static readonly int h_Attack        = Animator.StringToHash("Attack");
    private static readonly int h_WeaponType    = Animator.StringToHash("WeaponType");
    private static readonly int h_CanAttack     = Animator.StringToHash("canAttack");
    private static readonly int h_ChangeWeapon  = Animator.StringToHash("ChangeWeapon");
    private static readonly int h_ComboIndex    = Animator.StringToHash("ComboIndex");
    private static readonly int h_IsHeavy       = Animator.StringToHash("IsHeavy");   // opcional


    private void Awake()
    {
        anim = GetComponent<Animator>();

        anim.SetBool(h_CanAttack, true);
        anim.SetInteger(h_WeaponType, (int)currentWeapon);
        UpdateWeaponVisibility();
    }

    public void OnLightAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) TryAttack(isHeavy: false);
    }

    public void OnHeavyAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) TryAttack(isHeavy: true);
    }

    public void OnChangeWeapon(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (!anim.GetBool(h_CanAttack)) return;
        ToggleWeapon();
    }

    private void TryAttack(bool isHeavy)
    {
        // Reiniciar combo si tardó demasiado
        if (Time.time - lastAttackTime > comboResetTime)
            comboIndex = 0;

        if (comboIndex > 0 && !comboWindowOpen) return;

        anim.SetInteger(h_ComboIndex, comboIndex + 1);
        anim.SetBool   (h_IsHeavy, isHeavy);
        anim.SetTrigger(h_Attack);
        anim.SetBool   (h_CanAttack,  false);

        comboIndex = (comboIndex + 1) % maxCombo;
        lastAttackTime = Time.time;
        comboWindowOpen = false;
    }

    private void ToggleWeapon()
    {
        currentWeapon = currentWeapon == WeaponType.Punch ? WeaponType.Sword : WeaponType.Punch;
        anim.SetTrigger(h_ChangeWeapon);
        anim.SetInteger(h_WeaponType, (int)currentWeapon);

        // Aquí podrías reproducir VFX de desenvaine / envainar
        UpdateWeaponVisibility();
    }

    public void AE_EnableComboWindow() => comboWindowOpen = true;
    public void AE_EndAttack()
    {
        comboWindowOpen = false;
        anim.SetBool(h_CanAttack, true);
    }
    private void UpdateWeaponVisibility()
    {
        if (Sword != null)
            Sword.SetActive(currentWeapon == WeaponType.Sword);
    }
}
