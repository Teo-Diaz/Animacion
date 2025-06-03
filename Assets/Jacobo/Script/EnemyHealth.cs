using System.Collections; 
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageReceiver
{
    [SerializeField] private int maxHealth = 100;
    private Animator animator;
    [SerializeField] private float currentHealth;
    public GameObject panelWin; 


    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void ReceiveDamage(IDamageSender perpetrator, DamagePayload payload)
    {
        currentHealth -= payload.damageAmount;
        Debug.Log($"{gameObject.name} recibió {payload.damageAmount} de daño. Vida restante: {currentHealth}");
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hit"); // Animación de daño si sigue vivo
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        animator.SetTrigger("Die");
        animator.SetBool("Dead", true);
        panelWin.SetActive(true); 

        StartCoroutine(WaitAndDisable());

    }
    
    private IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(2.0f);

        if (TryGetComponent<EnemyState>(out var ai))
            ai.enabled = false;

        if (TryGetComponent<UnityEngine.AI.NavMeshAgent>(out var agent))
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        this.enabled = false;
    }
    
}
