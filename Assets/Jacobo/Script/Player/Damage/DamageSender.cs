using UnityEngine;

public class DamageSender : MonoBehaviour, IDamageSender
{
    public GameObject owner;
    public int damage = 10;
    public DamagePayload.DamageSeverity severity = DamagePayload.DamageSeverity.Light;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageReceiver>(out var receiver))
        {
            // Evitar dañarse a uno mismo
            if (receiver == owner.GetComponent<IDamageReceiver>())
                return;

            DamagePayload payload = new DamagePayload
            {
                position = transform.position,
                damageAmount = damage,
                severity = severity
            };

            receiver.ReceiveDamage(this, payload);
        }
    }

    public void SendDamage(IDamageReceiver target)
    {
        DamagePayload payload = new DamagePayload
        {
            position = transform.position,
            damageAmount = damage,
            severity = severity
        };

        target.ReceiveDamage(this, payload);
    }
}
