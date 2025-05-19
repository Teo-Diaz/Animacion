using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]
public class CharacterAim : MonoBehaviour, ICharacterComponent
{

    [SerializeField] private CinemachineCamera aimingCamera;
    [SerializeField] private AimConstraint aimConstraint;
    [SerializeField] private FloatDampener aimDampener;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        if(!ctx.started && !ctx.canceled) return;
        aimingCamera?.gameObject.SetActive(ctx.started);
        ParentCharacter.IsAiming = ctx.started;
        aimConstraint.enabled = ctx.started;
        aimDampener.TargetValue = ctx.started ? 1 : 0;
    }

    void Update()
    {
        aimDampener.Update();
        aimConstraint.weight = aimDampener.CurrentValue;
        anim.SetLayerWeight(1, aimDampener.CurrentValue);
        anim.SetLayerWeight(2, aimDampener.CurrentValue);
    }

    public Character ParentCharacter { get; set; }
}
