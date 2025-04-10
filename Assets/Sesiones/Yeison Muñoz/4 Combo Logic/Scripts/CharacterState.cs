using UnityEngine;

//stores all dynamic variables for the character
public class CharacterState : MonoBehaviour
{
    [SerializeField] private float startStamina;
    [SerializeField] private float staminaRegen;

    private float currentStamina;
    private float currentHealth;

    private void RegenerateStamina(float regenAmount)
    {
        currentStamina = Mathf.Min(currentStamina + regenAmount, startStamina);
    }

    public void DepleteStamina(float amount)
    {
        currentStamina -= amount;
    }

    private void Update()
    {
        RegenerateStamina(staminaRegen * Time.deltaTime);
    }
}
