using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageReceiver
{
    [SerializeField] private int maxHealth = 100;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void ReceiveDamage(IDamageSender perpetrator, DamagePayload payload)
    {
        currentHealth -= payload.damageAmount;
        Debug.Log($"{gameObject.name} recibió {payload.damageAmount} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        // Aquí podrías reproducir una animación de muerte o destruir el objeto
        Destroy(gameObject);
    }
}
