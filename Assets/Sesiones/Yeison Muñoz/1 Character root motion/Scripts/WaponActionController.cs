using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class WaponActionController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnWeaponAction(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;
        animator.SetTrigger("WeaponAction");

        //Activar el sistema de disparo
        
        //Activar el sistema de disparo
        //Machetazo:
        //Spawn proyectil
        //Mover Proyectil

        //Robusto:
        //Acceder a algun nexo de datos que se refieran al arma (puede ser un component especifico para el arma que tenga equipada el personaje)
        // con el componente del arma, se activa su funcion de accionarse



    }
}
