using UnityEngine;

public interface IDamageReceiver
{
    void ReceiveDamage(IDamageSender perpetrator, DamagePayload payload);
}
