using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageCube : MonoBehaviour, IDamageSender
{
    [SerializeField] private int faction = 99;
    [SerializeField] private float damage = 10f;
    [SerializeField] private DamagePayload.DamageSeverity severity = DamagePayload.DamageSeverity.Light;

    public int Faction => faction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageReceiver>(out IDamageReceiver receiver))
        {
            SendDamage(receiver); 
        }
    }

    public void SendDamage(IDamageReceiver target)
    {
        DamagePayload payload = new DamagePayload
        {
            damageAmount = damage, position = transform.position, severity = severity
        };

        target.ReceiveDamage(this, payload);
    }
}
