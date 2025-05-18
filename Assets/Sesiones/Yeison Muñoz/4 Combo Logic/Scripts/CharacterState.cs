using UnityEngine;

// summary
//stores all dynamic variables for the character
// summary
public class CharacterState : MonoBehaviour
{
    [SerializeField] private float startStamina;
    [SerializeField] private float staminaRegen;
    [SerializeField] private float startHealth;

    [SerializeField] private float currentStamina;
    [SerializeField] private float currentHealth;

    private void RegenerateStamina(float regenAmount)
    {
        currentStamina = Mathf.Min(currentStamina + regenAmount, startStamina);
    }

    private float GetStaminaDepletion()
    {
        //sistema de inventario *1/stat_fuerza * 1/buff_fuerza
        return 60;
    }

    public void DepleteHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            #warning TODO Death
            //se murio
            Debug.Log($"({name}) DEAD");
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
