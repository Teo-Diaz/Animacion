using Mono.Cecil;
using UnityEngine;

public class Hitbox : MonoBehaviour, IDamageSender<DamageMessage>
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageReceiver<DamageMessage> receiver))
        {
            SendDamage(receiver);
        }
    }

    public void SendDamage(IDamageReceiver<DamageMessage> receiver)
    {
        DamageMessage message = new DamageMessage
        {
            sender = this.gameObject,
            amount = damage
        };
        receiver.ReceiveDamage(message);
    }
}
