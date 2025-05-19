using UnityEngine;

// summary
//stores all dynamic variables for the character
// summary
public class CharacterState : MonoBehaviour
{
    [SerializeField] private float startStamina = 1000.0f;
    [SerializeField] private float staminaRegen = 1000.0f;
    [SerializeField] private float startHealth = 100.0f;

    [SerializeField] private float currentStamina;
    [SerializeField] private float currentHealth = 100.0f;

    private void RegenerateStamina(float regenAmount)
    {
        currentStamina = Mathf.Min(currentStamina + regenAmount, startStamina);
    }

    private float GetStaminaDepletion()
    {
        //sistema de inventario *1/stat_fuerza * 1/buff_fuerza
        return 60;
    }

    public void DepleteHealth(float amount, out bool zeroHealth)
    {
        currentHealth -= amount;
        zeroHealth = false;
        if (currentHealth <= 0)
        {
            zeroHealth = true;
        }
    }

    public void DepleteStamina(float amount)
    {
        currentStamina -= GetStaminaDepletion() * amount;
    }

    private void Start()
    {
        currentStamina = startStamina;
    }

    private void Update()
    {
        RegenerateStamina(staminaRegen * Time.deltaTime);
    }

    public float CurrentStamina => currentStamina;
}
