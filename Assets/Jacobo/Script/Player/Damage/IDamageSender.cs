using UnityEngine;

public interface IDamageSender
{
    void SendDamage(IDamageReceiver target);
}
