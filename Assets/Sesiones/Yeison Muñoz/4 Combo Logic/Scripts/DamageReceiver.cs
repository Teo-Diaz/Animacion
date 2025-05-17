using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageReceiver<float>
{
    public void ReceiveDamage(float damage)
    {
        //Reducir vida del personaje
        //Activar muerte si la vida es baja
        Debug.Log("muelto");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
